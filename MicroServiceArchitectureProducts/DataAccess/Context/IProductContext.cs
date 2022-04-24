using MicroServiceArchitectureProducts.Entities;
using MongoDB.Driver;

namespace MicroServiceArchitectureProducts.DataAccess.Context
{
    public interface IProductContext
    {
        IMongoCollection<Product> Products { get; set; }
    }
}
