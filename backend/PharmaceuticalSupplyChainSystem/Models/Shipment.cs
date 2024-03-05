namespace PharmaceuticalSupplyChainSystem.Models
{
    public class Shipment
    {
        public string ShipmentID { get; set; }
        public string OrderID { get; set; }
        public string Carrier { get; set; }
        public string TrackingNumber { get; set; }
        public string CurrentLocation { get; set; }
        public DateOnly EstimatedDelivery { get; set; }
    }
}
