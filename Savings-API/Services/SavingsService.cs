using Microsoft.EntityFrameworkCore;
using Savings_API.Context;
using Savings_API.DTOs;

namespace Savings_API.Services
{
    public interface ISavingsService 
    {
        public IList<Saving> GetAllSavings();
        public IList<Saving> GetSavingsForYear(int year);
        public IList<Saving> GetSavingsForMonth(int year, int month);
    }

    public class SavingsService : BaseService, ISavingsService
    {
        public SavingsService(AppDbContext context) : base(context) { }

        public IList<Saving> GetAllSavings()
        {
            List<Saving> savings = _dbContext.Savings.AsNoTracking().ToList();

            return savings;
        }

        public IList<Saving> GetSavingsForYear(int year)
        {
            List<Saving> savings = _dbContext.Savings.Where(s => s.Date.Value.Year == year).AsNoTracking().ToList();

            return savings;
        }

        public IList<Saving> GetSavingsForMonth(int year, int month)
        {
            List<Saving> savings = _dbContext.Savings.Where(s => s.Date.Value.Month == month && s.Date.Value.Year == year).AsNoTracking().ToList();

            return savings;
        }

        //public Saving AddSaving(AddSavingDto dto)
        //{

        //}
    }
}
