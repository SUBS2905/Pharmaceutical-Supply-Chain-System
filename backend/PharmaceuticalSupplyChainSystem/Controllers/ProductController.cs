using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmaceuticalSupplyChainSystem.Models;
using PharmaceuticalSupplyChainSystem.Services;

namespace PharmaceuticalSupplyChainSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [Authorize(Roles = "admin,manufacturer,regulatoryAuthority")]
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await _productService.GetAllProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "admin,manufacturer,regulatoryAuthority")]
        [HttpGet("productById")]
        public async Task<IActionResult> GetProductById([FromQuery(Name = "id")] string id)
        {
            try
            {
                var product = await _productService.GetProductById(id);
                if(product == null)
                {
                    return BadRequest("Product not found!");
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "admin,manufacturer,regulatoryAuthority")]
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            try
            {
                await _productService.AddProduct(product);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "admin,manufacturer,regulatoryAuthority")]
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            try
            {
                var updated = await _productService.UpdateProduct(product);
                if (updated == null)
                    return BadRequest("No product to update!");

                return Ok(product);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "admin,manufacturer,regulatoryAuthority")]
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct([FromQuery] string productID)
        {
            try
            {
                var deleted = await _productService.DeleteProduct(productID);
                if (deleted == null)
                    return BadRequest("Nothing to delete!");
                return Ok(deleted);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
