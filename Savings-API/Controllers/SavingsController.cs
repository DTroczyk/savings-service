using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
