using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Savings_API.Context;
using Savings_API.DTOs;
using Savings_API.Services;

namespace Savings_API.Controllers;

[Route("api/savings")]
[ApiController]
public class SavingsController : ControllerBase
{
    private ISavingsService _service;

    public SavingsController(ISavingsService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    [Authorize] // For testing
    public IActionResult GetSaving(int id)
    {
        Saving saving = _service.GetSaving(id);

        if (saving == null)
        {
            return NotFound();
        }
        return Ok(saving);
    }

    [HttpGet]
    public IActionResult GetSavings()
    {
        IList<Saving> savings = _service.GetAllSavings();
        return Ok(savings);
    }

    [HttpGet("period/{year}")]
    public IActionResult GetSavings(int year)
    {
        IList<Saving> savings = _service.GetSavingsForYear(year);
        return Ok(savings);
    }

    [HttpGet("period/{year}/{month}")]
    public IActionResult GetSavings(int year, int month)
    {
        IList<Saving> savings = _service.GetSavingsForMonth(year, month);
        return Ok(savings);
    }

    [HttpPost]
    public async Task<IActionResult> AddSaving([FromBody] AddOrEditSavingDto payload)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(payload);
        }
        Saving newSaving = await _service.AddSaving(payload);
        return CreatedAtAction(nameof(GetSaving), new { id = newSaving.Id }, newSaving);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSaving([FromBody] AddOrEditSavingDto payload, int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(payload);
        }

        try
        {
            var updatedSaving = await _service.UpdateSaving(id, payload);
            return Ok(updatedSaving);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSaving(int id)
    {
        try
        {
            await _service.DeleteSaving(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
