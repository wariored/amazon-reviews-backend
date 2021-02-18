using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using Review.Domain.Models;
using MongoDB.Driver;
using Review.Domain.Interfaces.DBSettings;


namespace Review.Infrastructure.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _productsCollection;

        public ProductService(IProductsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _productsCollection = database.GetCollection<Product>(settings.ProductsCollectionName);
        }

        public async Task<IEnumerable<Product>> GetListAsync(string sort, int size)
        {
            var filter = Builders<Product>.Filter.Empty;
            var bsonSort = sort == "asc" ? Builders<Product>.Sort.Ascending("CreatedDate") : Builders<Product>.Sort.Descending("CreatedDate");
            var options = new FindOptions<Product>()
            {
                Sort = bsonSort,
                Limit = size
            };
            var result = await _productsCollection.FindAsync(filter, options).Result.ToListAsync();
            return result;
        }

        public async Task<IEnumerable<Product>> GetListByProductIdAsync(string productId) =>
           await _productsCollection.FindAsync(product => product.ProductId == productId).Result.ToListAsync();

        public async Task<Product> CreateAsync(Product product)
        {
            await _productsCollection.InsertOneAsync(product);
            return product;
        }

        public void Update(string id, Product productIn) =>
            _productsCollection.ReplaceOne(product => product.ProductId == id, productIn);

        public async Task RemoveAsync(Product productIn) =>
            await _productsCollection.DeleteOneAsync(product => product.Id == productIn.Id);

        public  async Task RemoveAsync(string id) =>
           await _productsCollection.DeleteOneAsync(product => product.Id == id);
    }
}