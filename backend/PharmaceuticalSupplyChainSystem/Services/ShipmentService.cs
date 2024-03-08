using PharmaceuticalSupplyChainSystem.Models;
using PharmaceuticalSupplyChainSystem.Models.RequestModels;
using PharmaceuticalSupplyChainSystem.Repositories.Interfaces;
using PharmaceuticalSupplyChainSystem.Services.Interfaces;

namespace PharmaceuticalSupplyChainSystem.Services
{
    public class ShipmentService: IShipmentService
    {
        private readonly IShipmentRepository _shipmentRepository;
        public ShipmentService(IShipmentRepository shipmentRepository)
        {
            _shipmentRepository = shipmentRepository;
        }

        public async Task<Shipment[]> GetAllShipments()
        {
            try
            {
                return await _shipmentRepository.GetAllShipments();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<Shipment> GetShipmentById(string trackingNo)
        {
            try
            {
                return await _shipmentRepository.GetShipmentById(trackingNo);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<Shipment> AddShipment(ShipmentRequest req)
        {
            try
            {
                return await _shipmentRepository.AddShipment(req);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
    }
}
