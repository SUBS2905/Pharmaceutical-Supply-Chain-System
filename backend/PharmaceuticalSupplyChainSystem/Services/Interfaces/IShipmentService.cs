using PharmaceuticalSupplyChainSystem.Models;
using PharmaceuticalSupplyChainSystem.Models.RequestModels;

namespace PharmaceuticalSupplyChainSystem.Services.Interfaces
{
    public interface IShipmentService
    {
        public async Task<Shipment[]> GetAllShipments()
        {
            throw new NotImplementedException();
        }

        public async Task<Shipment> GetShipmentById(string trackingNo)
        {
            throw new NotImplementedException();
        }

        public async Task<Shipment> AddShipment(ShipmentRequest req)
        {
            throw new NotImplementedException();
        }
    }
}
