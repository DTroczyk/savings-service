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
        public Saving GetSaving(int savingId);
        public Task<Saving> AddSaving(AddSavingDto dto);
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

        public async Task<Saving> AddSaving(AddSavingDto dto)
        {
            Saving newSaving = new Saving
            {
                InsertDate = DateTime.UtcNow,
                Amount = dto.Amount,
                Description = dto.Description,
                Goal = dto.Goal,
                Date = dto.Date
            };

            await _dbContext.Savings.AddAsync(newSaving);
            await _dbContext.SaveChangesAsync();

            return newSaving;
        }

        public Saving GetSaving(int savingId)
        {
            Saving? saving = _dbContext.Savings.FirstOrDefault(s => s.Id == savingId);

            return saving;
        }
    }
}
