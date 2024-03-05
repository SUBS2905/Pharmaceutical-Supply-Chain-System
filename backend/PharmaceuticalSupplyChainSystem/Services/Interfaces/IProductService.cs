using PharmaceuticalSupplyChainSystem.Models;

namespace PharmaceuticalSupplyChainSystem.Services.Interfaces
{
    public interface IProductService
    {
        public async Task<Product[]> GetAllProducts()
        {
            throw new NotImplementedException();
        }
        public async Task<Product> GetProductById(string id)
        {
            throw new NotImplementedException();
        }
        public async Task<Product> AddProduct(Product product)
        {
            throw new NotImplementedException();
        }
        public async Task<Product> UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
        public async Task<Product> DeleteProduct(string id)
        {
            throw new NotImplementedException();
        }
    }
}
