using Footbally.Application.DTOs.User;
using Footbally.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Footbally.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponseDto>> GetById(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null) return NotFound(new { message = "Kullanıcı bulunamadı." });
        return Ok(user);
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserResponseDto>> Register([FromBody] RegisterRequestDto dto)
    {
        var existing = await _userService.GetByEmailAsync(dto.Email);
        if (existing != null)
            return Conflict(new { message = "Bu email zaten kayıtlı." });

        var user = await _userService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _userService.DeleteAsync(id);
        if (!result) return NotFound(new { message = "Kullanıcı bulunamadı." });
        return NoContent();
    }
}