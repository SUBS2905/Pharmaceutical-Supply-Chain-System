using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmaceuticalSupplyChainSystem.Models.RequestModels;
using PharmaceuticalSupplyChainSystem.Services;
using System.Text.Json;

namespace PharmaceuticalSupplyChainSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly InventoryService _inventoryService;
        public InventoryController(InventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [Authorize(Roles = "admin,manufacturer,distributor,retailer")]
        [HttpGet]
        public async Task<IActionResult> GetAllInventories()
        {
            try
            {
                var inventories = await _inventoryService.GetAllInventories();
                return Ok(inventories);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "admin,manufacturer,distributor")]
        [HttpPost]
        public async Task<IActionResult> AddInventory([FromBody] InventoryRequest req)
        {
            try
            {
                var inv = await _inventoryService.AddInventory(req);
                if (inv == null)
                    return NotFound("Cannot add inventory");

                return Ok(inv);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "admin,manufacturer,distributor")]
        [HttpDelete]
        public async Task<IActionResult> DeleteInventory([FromQuery] string batchNo)
        {
            try
            {
                var inv = await _inventoryService.DeleteInventory(batchNo);
                if (inv == null)
                    return BadRequest("Unable to delete inventory");
                
                return Ok(inv);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "admin,manufacturer,distributor,retailer")]
        [HttpPost("batchNo")]
        public async Task<IActionResult> GetInventoryByBatchNo([FromBody] JsonElement data)
        {
            try
            {
                string batchNo = data.GetProperty("batchNo").ToString();
                var inv = await _inventoryService.GetInventoryByBatchNo(batchNo);
                if (inv == null)
                    return NotFound("Inventory not found");

                return Ok(inv);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "admin,manufacturer,distributor,retailer")]
        [HttpPost("location")]
        public async Task<IActionResult> GetInventoryByLocation([FromBody] JsonElement data)
        {
            try
            {
                string location = data.GetProperty("location").ToString();
                var inv = await _inventoryService.GetInventoryByLocation(location);
                if (inv == null)
                    return NotFound("Inventory not found");

                return Ok(inv);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [Authorize(Roles = "admin,manufacturer,distributor,retailer")]
        [HttpPost("product")]
        public async Task<IActionResult> GetInventoryByProductId([FromBody] JsonElement data)
        {
            try
            {
                string productId = data.GetProperty("productId").ToString();
                var inv = await _inventoryService.GetInventoryByProductId(productId);
                if (inv == null)
                    return NotFound("Inventory not found");

                return Ok(inv);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "admin,manufacturer,distributor")]
        [HttpPut("quantity")]
        public async Task<IActionResult> UpdateQuantity(UpdateInventoryRequest req)
        {
            try
            {
                var inventory = await _inventoryService.UpdateQuantity(req);
                if (inventory == null)
                    return NotFound("Inventory not found");
                
                return Ok(inventory);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
