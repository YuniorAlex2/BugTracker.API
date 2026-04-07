using BugTracker.API.DTOs;
using BugTracker.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterUserDto dto)
    {
        var result = _authService.Register(dto);

        if (result == "User already exists.")
            return BadRequest(new { message = result });

        return Ok(new { message = result });
    }

    [HttpPost("login")]
    public IActionResult Login(LoginUserDto dto)
    {
        var token = _authService.Login(dto);

        if (token == null)
            return Unauthorized(new { message = "Invalid email or password." });

        return Ok(new { token });
    }
}