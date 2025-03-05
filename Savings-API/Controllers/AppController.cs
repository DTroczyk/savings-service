using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Savings_API.VMs;

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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult HealthCheck()
        {
            return Ok();
        }

        [HttpGet("version")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetApiVersion()
        {
            var version = _configuration["Version"];
            return Ok(new {Version = version});
        }
    }
}
