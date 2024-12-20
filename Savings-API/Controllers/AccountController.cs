﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Savings_API.DTOs;
using Savings_API.VMs;

namespace Savings_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IMapper _mapper;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await _userManager.FindByNameAsync(loginDto.UserName);
        if (user == null)
            return Unauthorized();

        var result = await _signInManager.PasswordSignInAsync(loginDto.UserName, loginDto.Password, false, lockoutOnFailure: false);

        if (!result.Succeeded)
            return Unauthorized("Invalid username or password.");

        return Ok("Login successed. JWT token will be added in the future.");
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = new IdentityUser { UserName = registerDto.UserName, Email = registerDto.Email };
        var passwordValidationResult = await _userManager.PasswordValidators.First().ValidateAsync(_userManager, user, registerDto.Password);

        if (!passwordValidationResult.Succeeded)
        {
            foreach (var error in passwordValidationResult.Errors)
            {
                ModelState.AddModelError("Password", error.Description);
            }
            return BadRequest(ModelState);
        }

        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return BadRequest(ModelState);
        }

        return Ok("User registered successfully");
    }

    /// <summary>
    /// Temporary endpoint until create JWT tokens.
    /// </summary>
    /// <returns></returns>
    [HttpGet("users")]
    public async Task<IActionResult> Users()
    {
        var users = await _userManager.Users.ToListAsync();

        List<UserVm> userVms = _mapper.Map<List<UserVm>>(users);

        return Ok(userVms);
    }
}
