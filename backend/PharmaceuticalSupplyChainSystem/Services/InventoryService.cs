using PharmaceuticalSupplyChainSystem.Models.RequestModels;
using PharmaceuticalSupplyChainSystem.Models;
using PharmaceuticalSupplyChainSystem.Repositories.Interfaces;
using PharmaceuticalSupplyChainSystem.Services.Interfaces;

namespace PharmaceuticalSupplyChainSystem.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;
        public InventoryService(IInventoryRepository inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public async Task<Inventory> AddInventory(InventoryRequest req)
        {
            try
            {
                return await _inventoryRepository.AddInventory(req);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<Inventory[]> GetAllInventories()
        {
            try
            {
                return await _inventoryRepository.GetAllInventories();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Inventory> GetInventoryByBatchNo(string batchNo)
        {
            try
            {
                return await _inventoryRepository.GetInventoryByBatchNo(batchNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Inventory> GetInventoryByLocation(string location)
        {
            try
            {
                return await _inventoryRepository.GetInventoryByLocation(location);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Inventory> GetInventoryByProductId(string productId)
        {
            try
            {
                return await _inventoryRepository.GetInventoryByProductId(productId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Inventory> DeleteInventory(string batchNo)
        {
            try
            {
                return await _inventoryRepository.DeleteInventory(batchNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Inventory> UpdateQuantity(UpdateInventoryRequest req)
        {
            try
            {
                return await _inventoryRepository.UpdateQuantity(req);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
