using PharmaceuticalSupplyChainSystem.Models;
using PharmaceuticalSupplyChainSystem.Repositories.Interfaces;
using PharmaceuticalSupplyChainSystem.Services.Interfaces;

namespace PharmaceuticalSupplyChainSystem.Services
{
    public class RecallService: IRecallService
    {
        private readonly IRecallRepository _recallRepository;
        public RecallService(IRecallRepository recallRepository)
        {
            _recallRepository = recallRepository;
        }

        public async Task<Recall[]> GetAllRecalls()
        {
            try
            {
                return await _recallRepository.GetAllRecalls();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<Recall> RecallProduct(string productId, string reason)
        {
            try
            {
                return await _recallRepository.RecallProduct(productId, reason);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
