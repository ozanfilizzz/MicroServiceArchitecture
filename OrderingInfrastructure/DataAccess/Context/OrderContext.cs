using Microsoft.EntityFrameworkCore;
using OrderingDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OrderingInfrastructure.DataAccess
{
    public class OrderContext: DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
           
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=OrderDb;Trusted_Connection=True");

        //}

        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().Property(o => o.TotalPrice)
                .IsRequired().HasColumnType("decimal");

            modelBuilder.Entity<Order>().Property(o => o.UnitPrice)
                .IsRequired().HasColumnType("decimal");
        }
    }
}
