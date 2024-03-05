using PharmaceuticalSupplyChainSystem.Models;
using System.Globalization;

namespace PharmaceuticalSupplyChainSystem.Repositories.Interfaces
{
    public interface IProductRepository
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
