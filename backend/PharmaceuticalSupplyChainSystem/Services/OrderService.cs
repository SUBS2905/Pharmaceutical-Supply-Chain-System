using PharmaceuticalSupplyChainSystem.Models;
using PharmaceuticalSupplyChainSystem.Repositories.Interfaces;
using PharmaceuticalSupplyChainSystem.Services.Interfaces;

namespace PharmaceuticalSupplyChainSystem.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Order[]> getAllOrders()
        {
            try
            {
                return await _orderRepository.getAllOrders();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<Order> getOrderByOrderId(string orderId)
        {
            try
            {
                return await _orderRepository.getOrderById(orderId);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public async Task<Order> addOrder(Order order)
        {
            try
            {
                return await _orderRepository.addOrder(order);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<Order> updateOrderStatus(string orderId, string newStatus)
        {
            try
            {
                return await _orderRepository.updateOrderStatus(orderId, newStatus);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<Order[]> getOrdersByCustomerId(string customerId)
        {
            try
            {
                return await _orderRepository.getOrdersByCustomerId(customerId);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
