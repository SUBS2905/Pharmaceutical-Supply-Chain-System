using PharmaceuticalSupplyChainSystem.Models;

namespace PharmaceuticalSupplyChainSystem.Services.Interfaces
{
    public interface IRecallService
    {
        public async Task<Recall[]> GetAllRecalls()
        {
            throw new NotImplementedException();
        }

        public async Task<Recall> RecallProduct(string productId, string reason)
        {
            throw new NotImplementedException();
        }
    }
}
