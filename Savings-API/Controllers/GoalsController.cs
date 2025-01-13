using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Savings_API.Context;
using Savings_API.DTOs;
using Savings_API.Services;

namespace Savings_API.Controllers;

[Route("api/goals")]
[ApiController]
public class GoalsController : ControllerBase
{
    private IGoalsService _service;

    public GoalsController(IGoalsService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public IActionResult GetGoal(int id)
    {
        Goal goal = _service.GetGoal(id);

        if (goal == null)
        {
            return NotFound();
        }
        return Ok(goal);
    }

    [HttpGet]
    public IActionResult GetGoals()
    {
        IList<Goal> goals = _service.GetAllGoals();
        return Ok(goals);
    }

    
}
