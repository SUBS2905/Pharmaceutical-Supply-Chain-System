namespace PharmaceuticalSupplyChainSystem.Models.RequestModels
{
    public class ShipmentRequest
    {
        public string ShipperID { get; set; }
        public string ShipperName { get; set; }
        public string TrackingNumber { get; set; }
        public string CurrentLocation { get; set; }
        public string[] OrderIds { get; set; }
        public DateTime EstimatedDelivery { get; set; }
    }
}
