using MicroServiceArchitectureSourcing.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceArchitectureSourcing.DataAccess.Repositories
{
    public interface IBidRepository
    {
        Task SendBid(Bid bid);
        Task<List<Bid>> GetBidsByAuctionId(string id);
        Task<List<Bid>> GetAllBidsByAuctionId(string id);
        Task<Bid> GetWinnerBid(string id);
    }
}
