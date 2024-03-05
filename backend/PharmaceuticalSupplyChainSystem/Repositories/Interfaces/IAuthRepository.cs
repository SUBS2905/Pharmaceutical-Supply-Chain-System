using PharmaceuticalSupplyChainSystem.Models;
using PharmaceuticalSupplyChainSystem.Models.RequestModels;

namespace PharmaceuticalSupplyChainSystem.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        public async Task<User> Register(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> Login(LoginRequest req)
        { 
            throw new NotImplementedException();
        }

        public async Task<User> VerifyOTP(OTPVerificationRequest req)
        {
            throw new NotImplementedException();
        }
        public async Task<User> getUserById(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
