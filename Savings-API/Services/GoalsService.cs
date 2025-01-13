using Microsoft.EntityFrameworkCore;
using Savings_API.Context;
using Savings_API.DTOs;

namespace Savings_API.Services
{
    public interface IGoalsService 
    {
        public IList<Goal> GetAllGoals();
        public Goal GetGoal(int goalId);
    }

    public class GoalsService : BaseService, IGoalsService
    {
        public GoalsService(AppDbContext context) : base(context) { }

        public IList<Goal> GetAllGoals()
        {
            List<Goal> goals = _dbContext.Goals.AsNoTracking().ToList();

            return goals;
        }

        public Goal GetGoal(int goalId)
        {
            Goal? goal = _dbContext.Goals.Find(goalId);

            return goal;
        }
    }
}
