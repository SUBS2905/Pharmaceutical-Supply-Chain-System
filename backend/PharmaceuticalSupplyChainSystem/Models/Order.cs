using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PharmaceuticalSupplyChainSystem.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string OrderID { get; set; }
        public string CustomerID { get; set; }      //userid of retailer
        public string ProductID { get; set; }       
        public int Quantity { get; set; }
        public string OrderStatus { get; set; }     //Placed, Shipped, Delivered, Cancelled

    }
}
