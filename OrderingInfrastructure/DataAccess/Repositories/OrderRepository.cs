using Microsoft.EntityFrameworkCore;
using OrderingDomain.DataAccess.Repositories;
using OrderingDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingInfrastructure.DataAccess.Repositories
{
    public class OrderRepository : EfEntityRepository<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext dbContext) : base(dbContext)
        {

        }
        public async Task<IEnumerable<Order>> GetOrdersBySellerUserName(string userName)
        {
            var orderList = await _dbContext.Orders
                      .Where(o => o.SellerUserName == userName)
                      .ToListAsync();

            return orderList;
        }
    }
}
