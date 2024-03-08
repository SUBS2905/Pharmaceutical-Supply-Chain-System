using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmaceuticalSupplyChainSystem.Models.RequestModels;
using PharmaceuticalSupplyChainSystem.Services;
using System.Text.Json;

namespace PharmaceuticalSupplyChainSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {
        private readonly ShipmentService _shipmentService;
        public ShipmentController(ShipmentService shipmentService)
        {
            _shipmentService = shipmentService;
        }

        [Authorize(Roles ="admin,manufacturer,distributor")]
        [HttpGet]
        public async Task<IActionResult> GetAllShipments()
        {
            try
            {
                var shipments = await _shipmentService.GetAllShipments();

                if (shipments == null)
                    return NotFound("No shipments found");

                return Ok(shipments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles ="admin,manufacturer,distributor")]
        [HttpPost("byTrackingNo")]
        public async Task<IActionResult> GetShipmentsByTrackingNo([FromBody] JsonElement data)
        {
            try
            {
                string trackingNo = data.GetProperty("trackingNo").ToString();

                var shipment = await _shipmentService.GetShipmentById(trackingNo);

                if (shipment == null)
                    return NotFound("shipment not found");

                return Ok(shipment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles ="admin,manufacturer,distributor")]
        [HttpPost]
        public async Task<IActionResult> AddShipment([FromBody] ShipmentRequest req)
        {
            try
            {
                var shipment = await _shipmentService.AddShipment(req);

                return Ok(shipment);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
