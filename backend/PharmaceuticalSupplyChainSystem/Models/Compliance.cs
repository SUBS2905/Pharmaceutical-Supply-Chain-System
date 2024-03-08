using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PharmaceuticalSupplyChainSystem.Models
{
    public class Compliance
    {
        public Boolean FDA { get; set; }
        public Boolean EMA { get; set; }
    }
}
