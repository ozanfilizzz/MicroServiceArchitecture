using MicroServiceArchitectureSourcing.Entities;
using MongoDB.Driver;

namespace MicroServiceArchitectureSourcing.DataAccess.Context
{
    public interface ISourcingContext
    {
        IMongoCollection<Auction> Auctions { get; }
        IMongoCollection<Bid> Bids { get; }
    }
}
