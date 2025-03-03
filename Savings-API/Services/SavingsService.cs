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
        public IList<SavingVm> GetSavings(SavingsFilterDto filter);
        public SavingVm? GetSavingVm(int savingId);
        public Saving? GetSaving(int savingId);
        public Task<SavingVm> AddSaving(AddOrEditSavingDto dto);
        public Task<SavingVm> UpdateSaving(int savingId, AddOrEditSavingDto dto);
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

        public IList<SavingVm> GetSavings(SavingsFilterDto filter)
        {
            var query = _dbContext.Savings.AsQueryable();

            if (filter.DateFrom.HasValue)
                query = query.Where(s => s.Date >= filter.DateFrom.Value);

            if (filter.DateTo.HasValue)
                query = query.Where(s => s.Date <= filter.DateTo.Value);

            if (filter.UserId.HasValue)
                query = query.Where(s => s.UserId == filter.UserId.Value);

            if (filter.GoalId.HasValue)
                query = query.Where(s => s.GoalId == filter.GoalId.Value);

            if (!string.IsNullOrEmpty(filter.Description))
                query = query.Where(s => s.Description.Contains(filter.Description));

            List<Saving> savings = [.. query.Include(s => s.User).Include(s => s.Goal).AsNoTracking()];

            IList<SavingVm> savingsVms = _mapper.Map<List<SavingVm>>(savings);

            return savingsVms;
        }

        public async Task<SavingVm> AddSaving(AddOrEditSavingDto dto)
        {
            Goal? selectedGoal = _goalsService.GetGoal(dto.GoalId) ?? throw new Exception("Selected goal not exist.");
            //ApplicationUser? selectedUser = _goalsService.GetGoal(dto.GoalId) ?? throw new Exception("Selected goal not exist.");

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

            SavingVm newSavingVm = _mapper.Map<SavingVm>(newSaving);

            return newSavingVm;
        }

        public SavingVm? GetSavingVm(int savingId)
        {
            Saving? saving = _dbContext.Savings.Include(s => s.User).Include(s => s.Goal).AsNoTracking().FirstOrDefault(s => s.Id == savingId);

            SavingVm savingVm = _mapper.Map<SavingVm>(saving);

            return savingVm;
        }

        public Saving? GetSaving(int savingId)
        {
            Saving? saving = _dbContext.Savings.Find(savingId);

            return saving;
        }

        public async Task<SavingVm> UpdateSaving(int savingId, AddOrEditSavingDto dto)
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
            editedSaving.UserId = dto.UserId;

            await _dbContext.SaveChangesAsync();
            SavingVm editedSavingVm = _mapper.Map<SavingVm>(editedSaving);

            return editedSavingVm;
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
