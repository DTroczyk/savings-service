using Savings_API.Context;
using System;

namespace Savings_API.Services
{
    public abstract class BaseService
    {
        protected readonly AppDbContext _dbContext;

        public BaseService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
