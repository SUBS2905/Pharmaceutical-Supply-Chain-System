using PharmaceuticalSupplyChainSystem.Models;
using PharmaceuticalSupplyChainSystem.Repositories.Interfaces;

namespace PharmaceuticalSupplyChainSystem.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepo;
        public ProductService(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<Product[]> GetAllProducts()
        {
            try
            {
                return await _productRepo.GetAllProducts();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public async Task<Product> GetProductById(string id)
        {
            try
            {
                var product = await _productRepo.GetProductById(id);
                return product;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<Product> AddProduct(Product product)
        {
            try
            {
                return await _productRepo.AddProduct(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            try
            {
                return await _productRepo.UpdateProduct(product);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<Product> DeleteProduct(string id)
        {
            try
            {
                return await _productRepo.DeleteProduct(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
