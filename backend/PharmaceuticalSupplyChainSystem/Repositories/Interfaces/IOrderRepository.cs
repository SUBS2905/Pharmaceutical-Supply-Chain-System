using PharmaceuticalSupplyChainSystem.Models;

namespace PharmaceuticalSupplyChainSystem.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        public async Task<Order[]> getAllOrders()
        {
            throw new NotImplementedException();
        }
        public async Task<Order> addOrder(Order order)
        {
            throw new NotImplementedException();
        }
        public async Task<Order> updateOrderStatus(string orderId, string newStatus)
        {
            throw new NotImplementedException();
        }

        public async Task<Order[]> getOrdersByCustomerId(string customerId)
        {
            throw new NotImplementedException();
        }
    }
}
