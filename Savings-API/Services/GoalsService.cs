using Microsoft.EntityFrameworkCore;
using Savings_API.Context;
using Savings_API.DTOs;

namespace Savings_API.Services
{
    public interface IGoalsService 
    {
        public IList<Goal> GetAllGoals();
        public Goal? GetGoal(int goalId);
        public Task<Goal> AddGoal(AddOrEditGoalDto dto);
        public Task<Goal> UpdateGoal(int goalId, AddOrEditGoalDto dto);
        public Task DeleteGoal(int goalId);

    }

    public class GoalsService : BaseService, IGoalsService
    {
        public GoalsService(AppDbContext context) : base(context) { }

        public IList<Goal> GetAllGoals()
        {
            List<Goal> goals = _dbContext.Goals.AsNoTracking().ToList();

            return goals;
        }

        public Goal? GetGoal(int goalId)
        {
            Goal? goal = _dbContext.Goals.Find(goalId);

            return goal;
        }

        public async Task<Goal> AddGoal(AddOrEditGoalDto dto)
        {
            Goal newGoal = new Goal
            {
                Name = dto.Name,
                Description = dto.Description,
                CreatedDate = DateTime.Now,
                Status = Enums.EntityStatusEnum.Active,
                //OwnerId = ,
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
            
            _dbContext.Goals.Remove(goal);
            await _dbContext.SaveChangesAsync();
        }
    }
}
