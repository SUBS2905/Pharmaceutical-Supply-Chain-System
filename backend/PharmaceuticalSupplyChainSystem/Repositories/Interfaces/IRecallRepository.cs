using PharmaceuticalSupplyChainSystem.Models;

namespace PharmaceuticalSupplyChainSystem.Repositories.Interfaces
{
    public interface IRecallRepository
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
