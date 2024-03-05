using PharmaceuticalSupplyChainSystem.Models.RequestModels;
using PharmaceuticalSupplyChainSystem.Models;
using PharmaceuticalSupplyChainSystem.Repositories.Interfaces;
using MongoDB.Driver;
using MongoDB.Bson;

namespace PharmaceuticalSupplyChainSystem.Repositories
{
    public class InventoryRepository: IInventoryRepository
    {
        private readonly IConfiguration _config;

        public InventoryRepository(IConfiguration config)
        {
            _config = config;
        }

        public string CalculateLevel(int qty)
        {
            if (qty < 10)
                return "critically low";
            else if (qty < 25)
                return "very low";
            else if (qty < 50)
                return "low";
            else
                return "good";
        }

        public async Task<Inventory> AddInventory(InventoryRequest req)
        {
            try
            {
                var mongoURI = _config["ConnectionStrings:MONGO_URI"];
                var client = new MongoClient(mongoURI);
                var db = client.GetDatabase("PharmaceuticalSupplyChain");
                var productCollection = db.GetCollection<Product>("products");
                var inventoryCollection = db.GetCollection<Inventory>("inventory");

                var product = await productCollection.Find(p => p.ProductID == req.ProductID).FirstOrDefaultAsync<Product>();
                var inventory = await inventoryCollection.Find(i => i.BatchNumber == req.BatchNumber).FirstOrDefaultAsync<Inventory>();
                
                Inventory inv = new Inventory();
                if(inventory != null)
                {
                    throw new Exception("Batch already exists");
                }
                if (product != null)
                {
                    inv.ProductDetails = product;
                    inv.Location = req.Location;
                    inv.Quantity = req.Quantity;
                    inv.BatchNumber = req.BatchNumber;
                    inv.SerialNumber = req.Serialnumber;

                    inv.Level = CalculateLevel(inv.Quantity);

                    await inventoryCollection.InsertOneAsync(inv);
                    return inv;
                }
                else
                    throw new Exception("Product does not exist!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<Inventory[]> GetAllInventories()
        {
            try
            {
                var mongoURI = _config["ConnectionStrings:MONGO_URI"];
                var client = new MongoClient(mongoURI);
                var db = client.GetDatabase("PharmaceuticalSupplyChain");
                var collection = db.GetCollection<Inventory>("inventory");

                var inventories = await collection.Find(new BsonDocument()).ToListAsync();
                return inventories.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public async Task<Inventory> GetInventoryByBatchNo(string batchNo)
        {
            try
            {
                var mongoURI = _config["ConnectionStrings:MONGO_URI"];
                var client = new MongoClient(mongoURI);
                var db = client.GetDatabase("PharmaceuticalSupplyChain");
                var collection = db.GetCollection<Inventory>("inventory");

                var inventory = await collection.Find(i => i.BatchNumber == batchNo).FirstOrDefaultAsync<Inventory>();

                if (inventory == null)
                    throw new Exception("Inventory not found!");

                return inventory;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public async Task<Inventory> GetInventoryByLocation(string location)
        {
            try
            {
                var mongoURI = _config["ConnectionStrings:MONGO_URI"];
                var client = new MongoClient(mongoURI);
                var db = client.GetDatabase("PharmaceuticalSupplyChain");
                var collection = db.GetCollection<Inventory>("inventory");

                var inventory = await collection.Find(i => i.Location == location).FirstOrDefaultAsync<Inventory>();

                if (inventory == null)
                    throw new Exception("Inventory not found!");

                return inventory;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public async Task<Inventory> GetInventoryByProductId(string productId)
        {
            try
            {
                var mongoURI = _config["ConnectionStrings:MONGO_URI"];
                var client = new MongoClient(mongoURI);
                var db = client.GetDatabase("PharmaceuticalSupplyChain");
                var collection = db.GetCollection<Inventory>("inventory");

                var inventory = await collection.Find(i => i.ProductDetails.ProductID == productId).FirstOrDefaultAsync<Inventory>();

                if (inventory == null)
                    throw new Exception("Inventory not found!");

                return inventory;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<Inventory> DeleteInventory(string batchNo)
        {
            try
            {
                var mongoURI = _config["ConnectionStrings:MONGO_URI"];
                var client = new MongoClient(mongoURI);
                var db = client.GetDatabase("PharmaceuticalSupplyChain");
                var collection = db.GetCollection<Inventory>("inventory");

                var inventory = await collection.Find(i => i.BatchNumber == batchNo).FirstOrDefaultAsync<Inventory>();

                if (inventory == null)
                    throw new Exception("Inventory not found!");

                var filter = Builders<Inventory>.Filter.Eq(i => i.BatchNumber, batchNo);
                await collection.DeleteOneAsync(filter);

                return inventory;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<Inventory> UpdateQuantity(UpdateInventoryRequest req)
        {
            try
            {

                var mongoURI = _config["ConnectionStrings:MONGO_URI"];
                var client = new MongoClient(mongoURI);
                var db = client.GetDatabase("PharmaceuticalSupplyChain");
                var collection = db.GetCollection<Inventory>("inventory");

                var inventory = await collection.Find(i => i.BatchNumber ==  req.BatchNumber).FirstOrDefaultAsync<Inventory>();
            
                if (inventory == null)
                    throw new Exception("Inventory not found!");

                inventory.Quantity += req.QuantityAdded - req.QuantitySubtracted;
                inventory.Level = CalculateLevel(inventory.Quantity);

                var updateQty = Builders<Inventory>.Update
                    .Set(i => i.Quantity, inventory.Quantity)
                    .Set(i => i.Level, inventory.Level);

                await collection.UpdateOneAsync(i => i.BatchNumber == req.BatchNumber, updateQty);

                return inventory;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

    }
}
