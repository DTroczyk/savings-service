using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Savings_API.Context;
using Savings_API.DTOs;
using Savings_API.VMs;

namespace Savings_API.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IMapper _mapper;

    public AccountController(IMapper mapper)
    {
        _mapper = mapper;
    }

    //[HttpGet("users")]
    //public async Task<IActionResult> Users()
    //{

    //    List<UserVm> userVms = _mapper.Map<List<UserVm>>(users);

    //    return Ok(userVms);
    //}
}
