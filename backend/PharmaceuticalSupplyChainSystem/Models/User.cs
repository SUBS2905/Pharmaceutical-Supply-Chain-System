using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PharmaceuticalSupplyChainSystem.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public string UserID { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public MultiFactorAuthData MFAData {get; set;}

    }
}
