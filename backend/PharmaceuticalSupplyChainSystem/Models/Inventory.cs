using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PharmaceuticalSupplyChainSystem.Models
{
    public class Inventory
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public Product ProductDetails { get; set; }     //Embedded Product Document
        public string Location { get; set; }
        public int Quantity { get; set; }
        public string Level { get; set; }
        public string BatchNumber { get; set; }
        public string SerialNumber { get; set; }
    }
}
