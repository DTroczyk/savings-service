using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Savings_API.Context;
using Savings_API.DTOs;
using Savings_API.VMs;

namespace Savings_API.Services
{
    public interface ISavingsService 
    {
        public IList<SavingVm> GetAllSavings();
        public IList<SavingVm> GetSavingsForYear(int year);
        public IList<SavingVm> GetSavingsForMonth(int year, int month);
        public SavingVm? GetSavingVm(int savingId);
        public Saving? GetSaving(int savingId);
        public Task<Saving> AddSaving(AddOrEditSavingDto dto);
        public Task<Saving> UpdateSaving(int savingId, AddOrEditSavingDto dto);
        public Task DeleteSaving(int savingId);
    }

    public class SavingsService : BaseService, ISavingsService
    {
        private readonly IGoalsService _goalsService;
        private readonly IMapper _mapper;

        public SavingsService(AppDbContext context, IGoalsService goalsService, IMapper mapper) : base(context) 
        { 
            _goalsService = goalsService;
            _mapper = mapper;
        }

        public IList<SavingVm> GetAllSavings()
        {
            List<Saving> savings = _dbContext.Savings.Include(s => s.User).Include(s => s.Goal).AsNoTracking().ToList();

            IList<SavingVm> savingsVms = _mapper.Map<List<SavingVm>>(savings);

            return savingsVms;
        }

        public IList<SavingVm> GetSavingsForYear(int year)
        {
            List<Saving> savings = _dbContext.Savings.Where(s => s.Date.Year == year).Include(s => s.User).Include(s => s.Goal).AsNoTracking().ToList();

            IList<SavingVm> savingsVms = _mapper.Map<List<SavingVm>>(savings);

            return savingsVms;
        }

        public IList<SavingVm> GetSavingsForMonth(int year, int month)
        {
            List<Saving> savings = _dbContext.Savings.Where(s => s.Date.Month == month && s.Date.Year == year).Include(s => s.User).Include(s => s.Goal).AsNoTracking().ToList();

            IList<SavingVm> savingsVms = _mapper.Map<List<SavingVm>>(savings);

            return savingsVms;
        }

        public async Task<Saving> AddSaving(AddOrEditSavingDto dto)
        {
            Goal? selectedGoal = _goalsService.GetGoal(dto.GoalId) ?? throw new Exception("Selected goal not exist.");

            Saving newSaving = new Saving
            {
                InsertDate = DateTime.UtcNow,
                Amount = dto.Amount,
                Description = dto.Description,
                GoalId = dto.GoalId,
                Date = dto.Date,
                UserId = dto.UserId
            };

            await _dbContext.Savings.AddAsync(newSaving);
            await _dbContext.SaveChangesAsync();

            return newSaving;
        }

        public SavingVm? GetSavingVm(int savingId)
        {
            Saving? saving = _dbContext.Savings.Find(savingId);

            SavingVm savingVm = _mapper.Map<SavingVm>(saving);

            return savingVm;
        }

        public Saving? GetSaving(int savingId)
        {
            Saving? saving = _dbContext.Savings.Find(savingId);

            return saving;
        }

        public async Task<Saving> UpdateSaving(int savingId, AddOrEditSavingDto dto)
        {
            Goal? selectedGoal = _goalsService.GetGoal(dto.GoalId) ?? throw new Exception("Selected goal not exist.");

            Saving? editedSaving = GetSaving(savingId);
            if (editedSaving == null)
            {
                throw new KeyNotFoundException($"Saving with ID {savingId} not found");
            }

            editedSaving.GoalId = dto.GoalId;
            editedSaving.Description = dto.Description;
            editedSaving.Amount = dto.Amount;
            editedSaving.Date = dto.Date;

            await _dbContext.SaveChangesAsync();

            return editedSaving;
        }

        public async Task DeleteSaving(int savingId)
        {
            Saving? saving = GetSaving(savingId);
            if (saving == null)
            {
                throw new KeyNotFoundException($"Saving with ID {savingId} not found");
            }

            _dbContext.Savings.Remove(saving);
            await _dbContext.SaveChangesAsync();
        }
    }
}
