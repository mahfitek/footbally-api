using Footbally.Application.DTOs.Auth;
using Footbally.Application.DTOs.User;
using Footbally.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Footbally.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
            return BadRequest(new { message = "Email ve şifre zorunludur." });

        var result = await _authService.LoginAsync(dto);
        if (result == null)
            return Unauthorized(new { message = "Email veya şifre hatalı." });

        return Ok(result);
    }

    [HttpPost("register")]
    public async Task<ActionResult<LoginResponseDto>> Register([FromBody] RegisterRequestDto dto)
    {
        try
        {
            var result = await _authService.RegisterAsync(dto);
            return CreatedAtAction(nameof(Login), result);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }
}