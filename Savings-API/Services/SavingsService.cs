using Microsoft.EntityFrameworkCore;
using Savings_API.Context;

namespace Savings_API.Services
{
    public interface ISavingsService 
    {
        public IList<Saving> GetAllSavings();
    }

    public class SavingsService : BaseService, ISavingsService
    {
        public SavingsService(AppDbContext context) : base(context) { }

        public IList<Saving> GetAllSavings()
        {
            List<Saving> savings = _dbContext.Savings.ToList<Saving>();

            return savings;
        }
    }
}
