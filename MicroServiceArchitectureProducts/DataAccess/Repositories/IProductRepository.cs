using MicroServiceArchitectureProducts.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceArchitectureProducts.DataAccess.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAll();
        Task<Product> GetById(string id);
        Task<IEnumerable<Product>> GetByProductName(string name);
        Task<IEnumerable<Product>> GetProductsByCategoryName(string categoryName);

        Task Create(Product product);
        Task<bool> Update(Product product);
        Task<bool> Delete(string id);
    }
}
