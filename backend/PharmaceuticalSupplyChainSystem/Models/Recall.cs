using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PharmaceuticalSupplyChainSystem.Models
{
    public class Recall
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string RecallID { get; set; }
        public Product RecalledProduct { get; set; }
        public string Reason { get; set; }
    }
}
