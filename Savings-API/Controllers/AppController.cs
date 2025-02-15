using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Savings_API.Controllers
{
    [Route("api")]
    [ApiController]
    public class AppController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public AppController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet("health-check")]
        public IActionResult HealthCheck()
        {
            return Ok();
        }

        [HttpGet("version")]
        public IActionResult GetApiVersion()
        {
            var version = _configuration["Version"];
            return Ok(new {Version = version});
        }
    }
}
