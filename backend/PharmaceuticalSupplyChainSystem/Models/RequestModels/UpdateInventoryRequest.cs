namespace PharmaceuticalSupplyChainSystem.Models.RequestModels
{
    public class UpdateInventoryRequest
    {
        public string BatchNumber { get; set; }
        public int QuantityAdded { get; set; }
        public int QuantitySubtracted { get; set; }
    }
}
