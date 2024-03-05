using PharmaceuticalSupplyChainSystem.Models;
using PharmaceuticalSupplyChainSystem.Models.RequestModels;
using PharmaceuticalSupplyChainSystem.Repositories.Interfaces;
using PharmaceuticalSupplyChainSystem.Services.Interfaces;

namespace PharmaceuticalSupplyChainSystem.Services
{
    public class AuthService: IAuthService
    {
        private readonly IAuthRepository _authRepo;
        public AuthService(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

        public async Task<User> Register(User user)
        {
            try
            {
                await _authRepo.Register(user);
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> Login(LoginRequest req)
        {
            try
            {
                return await _authRepo.Login(req);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> VerifyOTP(OTPVerificationRequest req)
        {
            try
            {
                return await _authRepo.VerifyOTP(req);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<User> getUserById(string userId)
        {
            try
            {
                return await _authRepo.getUserById(userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
