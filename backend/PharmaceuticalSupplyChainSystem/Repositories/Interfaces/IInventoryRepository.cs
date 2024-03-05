using PharmaceuticalSupplyChainSystem.Models;
using PharmaceuticalSupplyChainSystem.Models.RequestModels;

namespace PharmaceuticalSupplyChainSystem.Repositories.Interfaces
{
    public interface IInventoryRepository
    {
        public async Task<Inventory[]> GetAllInventories()
        {
            throw new NotImplementedException();
        }
        public async Task<Inventory> GetInventoryByBatchNo(string batchNo)
        {
            throw new NotImplementedException();
        }
        public async Task<Inventory> GetInventoryByLocation(string location)
        {
            throw new NotImplementedException();
        }
        public async Task<Inventory> GetInventoryByProductId(string productId)
        {
            throw new NotImplementedException();
        }
        public async Task<Inventory> AddInventory(InventoryRequest req)
        {
            throw new NotImplementedException();
        }
        public async Task<Inventory> DeleteInventory(string batchNo)
        {
            throw new NotImplementedException();
        }
        public async Task<Inventory> UpdateQuantity(UpdateInventoryRequest req)
        {
            throw new NotImplementedException();
        }
    }
}
