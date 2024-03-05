namespace PharmaceuticalSupplyChainSystem.Models.RequestModels
{
    public class InventoryRequest
    {
        public string ProductID { get; set; }
        public string Location { get; set; }
        public int Quantity { get; set; }
        public string BatchNumber { get; set; }
        public string Serialnumber { get; set; }
    }
}
