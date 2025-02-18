using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Savings_API.Context;
using Savings_API.DTOs;
using Savings_API.Services;
using Savings_API.VMs;

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
        Goal? goal = _service.GetGoal(id);

        if (goal == null)
        {
            return NotFound();
        }
        return Ok(goal);
    }

    [HttpGet]
    public IActionResult GetGoals()
    {
        IList<GoalVm> goals = _service.GetAllGoals();
        return Ok(goals);
    }

    [HttpPost]
    public async Task<IActionResult> AddGoal([FromBody] AddOrEditGoalDto payload)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(payload);
        }
        Goal newGoal = await _service.AddGoal(payload);
        return CreatedAtAction(nameof(GetGoal), new { id = newGoal.Id }, newGoal);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateGoal([FromBody] AddOrEditGoalDto payload, int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(payload);
        }

        try
        {
            var updatedGoal = await _service.UpdateGoal(id, payload);
            return Ok(updatedGoal);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusDto dto)
    {
        try
        {
            await _service.UpdateStatus(id, dto.Status);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (BadHttpRequestException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
