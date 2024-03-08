using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PharmaceuticalSupplyChainSystem.Services;
using System.Text.Json;

namespace PharmaceuticalSupplyChainSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecallController : ControllerBase
    {
        private readonly RecallService _recallService;

        public RecallController(RecallService recallService)
        {
            this._recallService = recallService;
        }

        [Authorize(Roles ="admin,regulatoryAuthority")]
        [HttpGet]
        public async Task<IActionResult> GetAllRecalls()
        {
            try
            {
                var recall = await _recallService.GetAllRecalls();

                return Ok(recall);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles ="admin,regulatoryAuthority")]
        [HttpPost]
        public async Task<IActionResult> RecallProduct([FromBody] JsonElement data)
        {
            try
            {
                string productId = data.GetProperty("productId").ToString();
                string reason = data.GetProperty("reason").ToString();

                var recall = await _recallService.RecallProduct(productId, reason);

                if (recall == null)
                    return NotFound("Product not found");

                return Ok(recall);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
