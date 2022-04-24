using MicroServiceArchitectureUIDomain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServiceArchitectureUIInfrastructure.DataAccess.Context
{
    public class WebAppContext : IdentityDbContext<User>
    {
        public WebAppContext(DbContextOptions<WebAppContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
