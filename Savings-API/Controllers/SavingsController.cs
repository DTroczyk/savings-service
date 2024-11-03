using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("get-all")]
        public IActionResult GetAllSavings()
        {
            var savings = _service.GetAllSavings();
            return Ok(savings);
        }

        [HttpGet("{year}")]
        public IActionResult GetSavingsForYear(int year)
        {
            var savings = _service.GetSavingsForYear(year);
            return Ok(savings);
        }

        [HttpGet("{year}/{month}")]
        public IActionResult GetSavingsForMonth(int year, int month)
        {
            var savings = _service.GetSavingsForMonth(year, month);
            return Ok(savings);
        }

        [HttpPost("add-saving")]
        public IActionResult AddSaving([FromBody] AddSavingDto payload) { 
            return Ok(payload); }
    }
}
