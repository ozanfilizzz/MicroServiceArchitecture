using MicroServiceArchitectureUIDomain.DataAccess.Repositories;
using MicroServiceArchitectureUIDomain.Entities;
using MicroServiceArchitectureUIInfrastructure.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroServiceArchitectureUIInfrastructure.DataAccess.Repositories
{
    public class UserRepository : EfRepositoryBase<User>, IUserRepository
    {
        private readonly WebAppContext _dbContext;

        public UserRepository(WebAppContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
