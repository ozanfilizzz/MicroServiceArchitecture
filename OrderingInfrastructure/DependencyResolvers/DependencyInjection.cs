using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderingDomain.DataAccess.Repositories;
using OrderingInfrastructure.DataAccess;
using OrderingInfrastructure.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingInfrastructure.DependencyResolvers
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            #region UseInMemoryDatabase
            //services.AddDbContext<OrderContext>(opt => opt.UseInMemoryDatabase(databaseName: "InMemoryDb"),
            //                                 ServiceLifetime.Singleton,
            //                                 ServiceLifetime.Singleton);
            #endregion

            #region UseSqlServer
            services.AddDbContext<OrderContext>(options =>
                   options.UseSqlServer(
                       configuration.GetConnectionString("OrderConnection"),
                       b => b.MigrationsAssembly(typeof(OrderContext).Assembly.FullName)), ServiceLifetime.Singleton);
            #endregion

            #region AddRepositories
            services.AddTransient(typeof(IEntityRepository<>), typeof(EfEntityRepository<>));
            services.AddTransient<IOrderRepository, OrderRepository>();
            #endregion


            return services;
        }
    }
}
