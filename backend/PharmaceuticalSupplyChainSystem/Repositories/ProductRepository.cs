using MongoDB.Bson;
using MongoDB.Driver;
using PharmaceuticalSupplyChainSystem.Models;
using PharmaceuticalSupplyChainSystem.Repositories.Interfaces;

namespace PharmaceuticalSupplyChainSystem.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _config;

        public ProductRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<Product[]> GetAllProducts()
        {
            try
            {
                var mongoURI = _config["ConnectionStrings:MONGO_URI"];
                var client = new MongoClient(mongoURI);
                var db = client.GetDatabase("PharmaceuticalSupplyChain");
                var collection = db.GetCollection<Product>("products");

                var products = await collection.Find(new BsonDocument()).ToListAsync();
                var productsArray = products.ToArray();
                return productsArray;

            }catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

        }

        public async Task<Product> GetProductById(string id)
        {
            try
            {
                var mongoURI = _config["ConnectionStrings:MONGO_URI"];
                var client = new MongoClient(mongoURI);
                var db = client.GetDatabase("PharmaceuticalSupplyChain");
                var collection = db.GetCollection<Product>("products");

                var product = await collection.Find(p => p.ProductID == id).FirstOrDefaultAsync<Product>();

                return product;

            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        public async Task<Product> AddProduct(Product product)
        {
            try
            {
                var mongoURI = _config["ConnectionStrings:MONGO_URI"];
                var client = new MongoClient(mongoURI);
                var db = client.GetDatabase("PharmaceuticalSupplyChain");
                var collection = db.GetCollection<Product>("products");

                var prod = await collection.Find(p => p.ProductID == product.ProductID).FirstOrDefaultAsync<Product>();
                
                //check if product already exists
                if (prod == null)
                {
                    await collection.InsertOneAsync(product);
                    return product;
                }

                throw new Exception("Product already exists!");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            try
            {
                var mongoURI = _config["ConnectionStrings:MONGO_URI"];
                var client = new MongoClient(mongoURI);
                var db = client.GetDatabase("PharmaceuticalSupplyChain");
                var collection = db.GetCollection<Product>("products");

                var filter = Builders<Product>.Filter.Eq(p => p.ProductID, product.ProductID);

                var update = Builders<Product>.Update
                    .Set(p => p.ProductName, product.ProductName)
                    .Set(p => p.Description, product.Description)
                    .Set(p => p.Formulation, product.Formulation)
                    .Set(p => p.Ingredients, product.Ingredients)
                    .Set(p => p.ExpirationDate, product.ExpirationDate)
                    .Set(p => p.Compliance, product.Compliance);

                var res = await collection.UpdateOneAsync(filter, update);

                if (res.ModifiedCount == 1)
                    return product;

                return null;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<Product> DeleteProduct(string id)
        {
            try
            {
                var mongoURI = _config["ConnectionStrings:MONGO_URI"];
                var client = new MongoClient(mongoURI);
                var db = client.GetDatabase("PharmaceuticalSupplyChain");
                var collection = db.GetCollection<Product>("products");

                var product = await collection.Find(p => p.ProductID == id).FirstOrDefaultAsync<Product>();
                var filter = Builders<Product>.Filter.Eq(p => p.ProductID, id);

                if(product != null)
                    await collection.DeleteOneAsync(filter);

                return product;
            }
            catch(Exception ex)
            { 
                throw new Exception(ex.ToString());
            }
        }
    }
}
