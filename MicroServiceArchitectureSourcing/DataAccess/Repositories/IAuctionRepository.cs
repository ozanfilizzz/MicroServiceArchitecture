using MicroServiceArchitectureSourcing.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceArchitectureSourcing.DataAccess.Repositories
{
    public interface IAuctionRepository
    {
        Task<IEnumerable<Auction>> GetAll();
        Task<Auction> GetById(string id);
        Task<Auction> GetByAuctionName(string name);
        Task Create(Auction auction);
        Task<bool> Update(Auction auction);
        Task<bool> Delete(string id);
    }
}
