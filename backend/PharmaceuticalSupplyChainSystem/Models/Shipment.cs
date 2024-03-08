using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PharmaceuticalSupplyChainSystem.Models
{
    public class Shipment
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string ShipperID { get; set; }
        public string ShipperName { get; set; }
        public string TrackingNumber { get; set; }
        public string CurrentLocation { get; set; }
        public Order[] Orders { get; set; }
        public DateTime EstimatedDelivery { get; set; }
    }
}
