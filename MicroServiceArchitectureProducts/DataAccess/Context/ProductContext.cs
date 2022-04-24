using MicroServiceArchitectureProducts.Entities;
using MicroServiceArchitectureProducts.Settings;
using MongoDB.Driver;

namespace MicroServiceArchitectureProducts.DataAccess.Context
{
    public class ProductContext : IProductContext
    {
        public ProductContext(IProductDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Products = database.GetCollection<Product>(settings.CollectionName);
            ProductContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; set; }

    }
}
