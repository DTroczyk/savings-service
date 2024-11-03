using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Savings_API.Context;
using Savings_API.DTOs;
using Savings_API.Services;

namespace Savings_API.Controllers
{
    [Route("api/savings")]
    [ApiController]
    public class SavingsController : ControllerBase
    {
        private ISavingsService _service;

        public SavingsController(ISavingsService service)
        {
            _service = service;
        }

        [HttpGet("get-saving/{id}")]
        public IActionResult GetSaving(int id)
        {
            Saving saving = _service.GetSaving(id);

            if (saving == null)
            {
                return NotFound();
            }
            return Ok(saving);
        }

        [HttpGet("get-all")]
        public IActionResult GetAllSavings()
        {
            IList<Saving> savings = _service.GetAllSavings();
            return Ok(savings);
        }

        [HttpGet("{year}")]
        public IActionResult GetSavingsForYear(int year)
        {
            IList<Saving> savings = _service.GetSavingsForYear(year);
            return Ok(savings);
        }

        [HttpGet("{year}/{month}")]
        public IActionResult GetSavingsForMonth(int year, int month)
        {
            IList<Saving> savings = _service.GetSavingsForMonth(year, month);
            return Ok(savings);
        }

        [HttpPost("add-saving")]
        public async Task<IActionResult> AddSaving([FromBody] AddOrEditSavingDto payload)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(payload);
            }
            Saving newSaving = await _service.AddSaving(payload);
            return CreatedAtAction(nameof(GetSaving), new { id = newSaving.Id }, newSaving);
        }

        [HttpPut("update-saving/{id}")]
        public async Task<IActionResult> UpdateSaving([FromBody] AddOrEditSavingDto payload, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(payload);
            }

            try
            {
                var updatedSaving = await _service.UpdateSaving(id, payload);
                return Ok(updatedSaving); // Zwraca 200 OK z zaktualizowanym obiektem
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // Zwraca 404 Not Found
            }
        }
    }
}
