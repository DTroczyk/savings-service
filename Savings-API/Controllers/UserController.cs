using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Savings_API.Services;
using Savings_API.VMs;

namespace Savings_API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IList<UserVm>), StatusCodes.Status200OK)]
        public IActionResult GetUsers()
        {
            return Ok(_userService.GetUsers());
        }
    }
}
