using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PharmaceuticalSupplyChainSystem.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Formulation { get; set; }     //Tablet, Capsule, Syrup, Injection, etc.
        public string[] Ingredients { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Compliance Compliance { get; set; }
    }
}
