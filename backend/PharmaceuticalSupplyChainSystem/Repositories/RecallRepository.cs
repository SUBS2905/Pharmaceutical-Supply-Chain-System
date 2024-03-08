using MongoDB.Bson;
using MongoDB.Driver;
using PharmaceuticalSupplyChainSystem.Models;
using PharmaceuticalSupplyChainSystem.Repositories.Interfaces;

namespace PharmaceuticalSupplyChainSystem.Repositories
{
    public class RecallRepository : IRecallRepository
    {
        private readonly IConfiguration _config;
        private readonly IProductRepository _productRepository;

        public RecallRepository(IConfiguration config, IProductRepository productRepository)
        {
            _config = config;
            _productRepository = productRepository;
        }
        public async Task<Recall[]> GetAllRecalls()
        {
            try
            {
                var mongoURI = _config["ConnectionStrings:MONGO_URI"];
                var client = new MongoClient(mongoURI);
                var db = client.GetDatabase("PharmaceuticalSupplyChain");
                var collection = db.GetCollection<Recall>("recalls");

                var recalls = await collection.Find(new BsonDocument()).ToListAsync();

                return recalls.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<Recall> RecallProduct(string productId, string reason)
        {
            try
            {

                var mongoURI = _config["ConnectionStrings:MONGO_URI"];
                var client = new MongoClient(mongoURI);
                var db = client.GetDatabase("PharmaceuticalSupplyChain");
                var recallCollection = db.GetCollection<Recall>("recalls");

                var product = await _productRepository.GetProductById(productId);

                var recall = new Recall
                {
                    RecallID = Guid.NewGuid().ToString(),
                    RecalledProduct = product,
                    Reason = reason
                };

                await recallCollection.InsertOneAsync(recall);

                await _productRepository.DeleteProduct(product.ProductID);
                
                return recall;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
