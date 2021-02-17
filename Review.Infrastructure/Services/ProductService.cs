using System.Collections.Generic;
using Review.Domain.Models;

namespace Review.Infrastructure.Services
{
    public class ProductService
    {
        private readonly IMongoCollection<Product> _productsCollection;

        public ProductService(IProductDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _productsCollection = database.GetCollection<Product>(settings.BooksCollectionName);
        }

        public List<Product> Get() =>
            _productsCollection.Find(product => true).ToList();

        public Product Get(string id) =>
            _productsCollection.Find<Product>(product => product.Id == id).FirstOrDefault();

        public Product Create(Product product)
        {
            _productsCollection.InsertOne(product);
            return product;
        }

        public void Update(string id, Product productIn) =>
            _productsCollection.ReplaceOne(book => book.Id == id, productIn);

        public void Remove(Product productIn) =>
            _productsCollection.DeleteOne(product => product.Id == productIn.ProductId);

        public void Remove(string id) => 
            _productsCollection.DeleteOne(product => product.id == id);
    }

    }
}