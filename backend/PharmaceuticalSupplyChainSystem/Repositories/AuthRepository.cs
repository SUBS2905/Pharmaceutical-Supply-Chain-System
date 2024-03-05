using MongoDB.Bson;
using MongoDB.Driver;
using PharmaceuticalSupplyChainSystem.Models;
using PharmaceuticalSupplyChainSystem.Models.RequestModels;
using PharmaceuticalSupplyChainSystem.Repositories.Interfaces;

namespace PharmaceuticalSupplyChainSystem.Repositories
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(string message) : base(message) { }
    }
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException(string message) : base(message) { }
    }

    public class OTPExpiredException : Exception 
    {
        public OTPExpiredException(string message) : base(message) { }
    }

    public class AuthRepository: IAuthRepository
    {
        private readonly IConfiguration _config;
        private static readonly Random Random = new Random();
        public AuthRepository(IConfiguration config)
        {
            _config = config;    
        }

        private static string HashPassword(string password)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            return hashedPassword;
        }

        private static bool VerifyPassword(string password, string hashedPassword)
        {
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            return isPasswordValid;
        }

        private static string[] GenerateRecoveryCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            int numberOfRecoveryCodes = 3;
            string[] recoveryCodes = new string[numberOfRecoveryCodes];

            for (int i = 0; i < numberOfRecoveryCodes; i++)
            {
                var recoveryCode = new string(Enumerable.Repeat(chars, 6)
                    .Select(s => s[Random.Next(s.Length)]).ToArray());
                recoveryCodes[i] = recoveryCode;
            }

            return recoveryCodes;
        }

        private static string generateOTP()
        {
            var otp = new Random().Next(100000, 999999).ToString();
            return otp;
        }

        public async Task<User> Register(User user)
        {
            //Initialize the Database
            try
            {
                var mongoURI = _config["ConnectionStrings:MONGO_URI"];
                var client = new MongoClient(mongoURI);
                var db = client.GetDatabase("PharmaceuticalSupplyChain");
                var collection = db.GetCollection<User>("users");

                //Handle duplicate User
                var filter = Builders<User>.Filter.Eq(u => u.Email, user.Email);
                var existing = await collection.Find(filter).FirstOrDefaultAsync();

                if(existing != null)
                {
                    throw new Exception("User already exists");
                }

                user.UserID = Guid.NewGuid().ToString();
                user.Password = HashPassword(user.Password);
                user.MFAData.OTPSecretKey = Guid.NewGuid().ToString();
                user.MFAData.RecoveryCodes = GenerateRecoveryCode();
                user.MFAData.DeliveryEmail = user.Email;

                await collection.InsertOneAsync(user);

                return user;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<User> Login(LoginRequest req)
        {
            try
            {
                var mongoURI = _config["ConnectionStrings:MONGO_URI"];
                var client = new MongoClient(mongoURI);
                var db = client.GetDatabase("PharmaceuticalSupplyChain");
                var collection = db.GetCollection<User>("users");

                var user = await collection.Find(u =>  u.Email == req.email).FirstOrDefaultAsync<User>();

                //User Not found
                if (user == null)
                {
                    throw new UserNotFoundException("User not found!");
                }

                //Check credentials
                if(!VerifyPassword(req.password, user.Password))
                {
                    throw new InvalidCredentialsException("Invalid Credentials!");
                }

                //Generate OTP after authentication
                var otp = generateOTP();

                user.MFAData.OTPSecretKey = otp;
                user.MFAData.LastOTPGeneration = DateTime.UtcNow;
                user.MFAData.AttemptCounter = 0;

                // Update the user record in the database
                var updateDefinition = Builders<User>.Update
                    .Set(u => u.MFAData.OTPSecretKey, otp)
                    .Set(u => u.MFAData.LastOTPGeneration, DateTime.UtcNow)
                    .Set(u => u.MFAData.AttemptCounter, 0);

                await collection.UpdateOneAsync(u => u.UserID == user.UserID, updateDefinition);


                return user;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<User> VerifyOTP(OTPVerificationRequest req)
        {
            try
            {
                var mongoURI = _config["ConnectionStrings:MONGO_URI"];
                var client = new MongoClient(mongoURI);
                var db = client.GetDatabase("PharmaceuticalSupplyChain");
                var collection = db.GetCollection<User>("users");

                var user = await collection.Find(u => u.Email == req.Email).FirstOrDefaultAsync<User>();

                //User not found
                if (user == null)
                {
                    throw new UserNotFoundException("User not found!");
                }

                //Otp expired
                if(user.MFAData.LastOTPGeneration.AddMinutes(5) < DateTime.UtcNow)
                {
                    throw new OTPExpiredException("OTP has expired");
                }

                //verify otp
                if(user.MFAData.OTPSecretKey != req.OTP)
                {
                    user.MFAData.AttemptCounter++;

                    await collection.UpdateOneAsync(u => u.Id == user.Id, Builders<User>.Update.Set(u => u.MFAData.AttemptCounter, user.MFAData.AttemptCounter));

                    throw new InvalidCredentialsException("Invalid OTP");
                }

                //if otp is correct
                return user;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<User> getUserById(string userId)
        {
            try { 
            
                var mongoURI = _config["ConnectionStrings:MONGO_URI"];
                var client = new MongoClient(mongoURI);
                var db = client.GetDatabase("PharmaceuticalSupplyChain");
                var collection = db.GetCollection<User>("users");

                var filter = Builders<User>.Filter.Eq(u => u.UserID, userId);

                var user = await collection.Find(filter).FirstOrDefaultAsync();

                if (user == null)
                {
                    throw new UserNotFoundException("User not found");
                }

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
