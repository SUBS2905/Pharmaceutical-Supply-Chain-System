using MongoDB.Bson;
using MongoDB.Driver;
using PharmaceuticalSupplyChainSystem.Models;
using PharmaceuticalSupplyChainSystem.Models.RequestModels;
using PharmaceuticalSupplyChainSystem.Repositories.Interfaces;

namespace PharmaceuticalSupplyChainSystem.Repositories
{
    public class ShipmentRepository: IShipmentRepository
    {
        private readonly IConfiguration _config;
        private readonly IOrderRepository _orderRepository;
        public ShipmentRepository(IConfiguration config, IOrderRepository orderRepository)
        {
            _config = config;
            _orderRepository = orderRepository;
        }
        public async Task<Shipment[]> GetAllShipments()
        {
            try
            {
                var mongoURI = _config["ConnectionStrings:MONGO_URI"];
                var client = new MongoClient(mongoURI);
                var db = client.GetDatabase("PharmaceuticalSupplyChain");
                var collection = db.GetCollection<Shipment>("shipments");

                var shipments = await collection.Find(new BsonDocument()).ToListAsync();

                return shipments.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<Shipment> GetShipmentById(string trackingNo)
        {
            try
            {
                var mongoURI = _config["ConnectionStrings:MONGO_URI"];
                var client = new MongoClient(mongoURI);
                var db = client.GetDatabase("PharmaceuticalSupplyChain");
                var collection = db.GetCollection<Shipment>("shipments");

                var shipment = await collection.Find(s => s.TrackingNumber == trackingNo).FirstOrDefaultAsync<Shipment>();

                return shipment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        
        public async Task<Shipment> AddShipment(ShipmentRequest req)
        {
            try
            {
                var mongoURI = _config["ConnectionStrings:MONGO_URI"];
                var client = new MongoClient(mongoURI);
                var db = client.GetDatabase("PharmaceuticalSupplyChain");
                var shipmentsCollection = db.GetCollection<Shipment>("shipments");
                var ordersCollection = db.GetCollection<Order>("orders");

                Shipment shipment = new Shipment();

                shipment.ShipperID = req.ShipperID;
                shipment.ShipperName = req.ShipperName;
                shipment.TrackingNumber = Guid.NewGuid().ToString();
                shipment.CurrentLocation = req.CurrentLocation;
                shipment.EstimatedDelivery = req.EstimatedDelivery;

                var orders = new List<Order>();

                foreach(var orderId in req.OrderIds)
                {
                    Order order = await _orderRepository.getOrderById(orderId);
                    order.OrderStatus = "shipped";
                    orders.Add(order);

                    //Update the status in Orders collection as well
                    var update = Builders<Order>.Update.Set(o => o.OrderStatus, "shipped");
                    await ordersCollection.UpdateOneAsync(o => o.OrderID == orderId, update);
                }

                shipment.Orders = orders.ToArray();

                await shipmentsCollection.InsertOneAsync(shipment);

                return shipment;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
