using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Savings_API.Context;
using Savings_API.DTOs;
using Savings_API.VMs;

namespace Savings_API.Services
{
    public interface IGoalsService 
    {
        public IList<GoalVm> GetAllGoals();
        public Goal? GetGoal(int goalId);
        public GoalVm? GetGoalVm(int goalId);
        public Task<Goal> AddGoal(AddOrEditGoalDto dto);
        public Task<Goal> UpdateGoal(int goalId, AddOrEditGoalDto dto);
        public Task DeleteGoal(int goalId);

    }

    public class GoalsService : BaseService, IGoalsService
    {
        private IMapper _mapper;

        public GoalsService(AppDbContext context, IMapper mapper) : base(context) {
            _mapper = mapper;
        }

        public IList<GoalVm> GetAllGoals()
        {
            List<Goal> goals = _dbContext.Goals.AsNoTracking().ToList();

            List<GoalVm> goalVms = _mapper.Map<List<GoalVm>>(goals);

            return goalVms;
        }

        public Goal? GetGoal(int goalId)
        {
            Goal? goal = _dbContext.Goals.Find(goalId);

            return goal;
        }

        public GoalVm? GetGoalVm(int goalId)
        {
            Goal? goal = _dbContext.Goals.Find(goalId);

            GoalVm goalVm = _mapper.Map<GoalVm>(goal);

            return goalVm;
        }

        public async Task<Goal> AddGoal(AddOrEditGoalDto dto)
        {
            Goal newGoal = new Goal
            {
                Name = dto.Name,
                Description = dto.Description,
                CreatedDate = DateTime.UtcNow,
                Status = Enums.EntityStatusEnum.Active,
            };

            await _dbContext.Goals.AddAsync(newGoal);
            await _dbContext.SaveChangesAsync();

            return newGoal;
        }

        public async Task<Goal> UpdateGoal(int goalId, AddOrEditGoalDto dto)
        {
            Goal? editedGoal = GetGoal(goalId) ?? throw new KeyNotFoundException($"Goal with ID {goalId} not found");

            editedGoal.Name = dto.Name;
            editedGoal.Description = dto.Description;

            await _dbContext.SaveChangesAsync();

            return editedGoal;
        }

        public async Task DeleteGoal(int goalId)
        {
            Goal? goal = GetGoal(goalId) ?? throw new KeyNotFoundException($"Goal with ID {goalId} not found");

            goal.Status = Enums.EntityStatusEnum.Unactive;
            await _dbContext.SaveChangesAsync();
        }
    }
}
