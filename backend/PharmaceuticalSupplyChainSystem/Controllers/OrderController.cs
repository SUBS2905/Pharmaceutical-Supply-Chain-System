using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PharmaceuticalSupplyChainSystem.Models;
using PharmaceuticalSupplyChainSystem.Services;
using System.Text.Json;

namespace PharmaceuticalSupplyChainSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderService _orderService;
        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize(Roles ="admin,distributor")]
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var orders = await _orderService.getAllOrders();

                if (orders == null)
                    return NotFound("No orders found");

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles ="admin,distributor")]
        [HttpPost("getByOId")]
        public async Task<IActionResult> GetOrderByOrderId([FromBody] JsonElement data)
        {
            try
            {
                string orderId = data.GetProperty("orderId").ToString();

                var order = await _orderService.getOrderByOrderId(orderId);

                if (order == null)
                    return NotFound("Order not found");

                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles ="admin,retailer")]
        [HttpPost("getByCId")]
        public async Task<IActionResult> GetOrdersByCustomerId([FromBody] JsonElement data)
        {
            try
            {
                string customerId = data.GetProperty("customerId").ToString();

                var orders = await _orderService.getOrdersByCustomerId(customerId);

                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles ="admin,retailer")]
        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody]Order order)
        {
            try
            {
                var ord = await _orderService.addOrder(order);
                if(ord == null)
                {
                    return NotFound("Product not found");
                }
                return Ok(ord);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles ="admin,distributor")]
        [HttpPost("status")]
        public async Task<IActionResult> updateOrderStatus([FromBody] JsonElement data)
        {
            try
            {   
                string orderId = data.GetProperty("orderId").ToString();
                string newStatus = data.GetProperty("newStatus").ToString();

                var ord = await _orderService.updateOrderStatus(orderId, newStatus);

                return Ok(ord);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
