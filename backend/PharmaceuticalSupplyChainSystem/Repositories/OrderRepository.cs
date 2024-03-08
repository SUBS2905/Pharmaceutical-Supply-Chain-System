using MongoDB.Bson;
using MongoDB.Driver;
using PharmaceuticalSupplyChainSystem.Models;
using PharmaceuticalSupplyChainSystem.Models.RequestModels;
using PharmaceuticalSupplyChainSystem.Repositories.Interfaces;

namespace PharmaceuticalSupplyChainSystem.Repositories
{
    public class OrderRepository: IOrderRepository
    {
        private readonly IConfiguration _config;
        private readonly IInventoryRepository _inventoryRepository;

        public OrderRepository(IConfiguration config, IInventoryRepository invRepo)
        {
            _config = config;
            _inventoryRepository = invRepo;
        }

        public async Task<Order[]> getAllOrders()
        {
            try
            {
                var mongoURI = _config["ConnectionStrings:MONGO_URI"];
                var client = new MongoClient(mongoURI);
                var db = client.GetDatabase("PharmaceuticalSupplyChain");
                var collection = db.GetCollection<Order>("orders");

                var orders = await collection.Find(new BsonDocument()).ToListAsync();

                return orders.ToArray();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<Order> getOrderById(string orderId)
        {
            try
            {
                var mongoURI = _config["ConnectionStrings:MONGO_URI"];
                var client = new MongoClient(mongoURI);
                var db = client.GetDatabase("PharmaceuticalSupplyChain");
                var collection = db.GetCollection<Order>("orders");

                var order = await collection.Find(o => o.OrderID == orderId).FirstOrDefaultAsync<Order>();

                return order;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public async Task<Order> addOrder(Order order)
        {
            try
            {
                var mongoURI = _config["ConnectionStrings:MONGO_URI"];
                var client = new MongoClient(mongoURI);
                var db = client.GetDatabase("PharmaceuticalSupplyChain");
                var collection = db.GetCollection<Order>("orders");
                var inventoryCollection = db.GetCollection<Inventory>("inventory");

                var inv = await _inventoryRepository.GetInventoryByProductId(order.ProductID);

                if(inv == null)
                {
                    return null;    //Product not in inventory
                }

                if (inv.Quantity >= order.Quantity)
                {
                    UpdateInventoryRequest req = new UpdateInventoryRequest
                    {
                        BatchNumber = inv.BatchNumber,
                        QuantityAdded = 0,
                        QuantitySubtracted = order.Quantity,
                    };
                    await _inventoryRepository.UpdateQuantity(req);
                }
                else
                    return null;

                Order ord = new Order();

                ord.OrderID = Guid.NewGuid().ToString();
                ord.OrderStatus = "placed";
                ord.ProductID = order.ProductID;

                ord.Quantity = order.Quantity;
                ord.CustomerID = order.CustomerID;

                await collection.InsertOneAsync(ord);
                return (ord);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<Order> updateOrderStatus(string orderId, string newStatus)
        {
            try
            {
                var mongoURI = _config["ConnectionStrings:MONGO_URI"];
                var client = new MongoClient(mongoURI);
                var db = client.GetDatabase("PharmaceuticalSupplyChain");
                var collection = db.GetCollection<Order>("orders");

                var filter = Builders<Order>.Filter.Eq(o => o.OrderID, orderId);

                var update = Builders<Order>.Update.Set(o => o.OrderStatus, newStatus);

                var result = await collection.UpdateOneAsync(filter, update);


                if (result.ModifiedCount == 1)
                {
                    var updatedOrder = await collection.Find(filter).FirstOrDefaultAsync();
                    return updatedOrder;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }


        }

        public async Task<Order[]> getOrdersByCustomerId(string customerId)
        {
            try
            {
                var mongoURI = _config["ConnectionStrings:MONGO_URI"];
                var client = new MongoClient(mongoURI);
                var db = client.GetDatabase("PharmaceuticalSupplyChain");
                var collection = db.GetCollection<Order>("orders");

                var filter = Builders<Order>.Filter.Eq(o => o.CustomerID, customerId);
                var orders = await collection.Find(filter).ToListAsync();

                return orders.ToArray();

            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

    }
}
