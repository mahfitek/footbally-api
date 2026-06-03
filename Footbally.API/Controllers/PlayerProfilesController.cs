using Footbally.Application.DTOs.Player;
using Footbally.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Footbally.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayerProfilesController : ControllerBase
{
    private readonly IPlayerProfileService _playerProfileService;

    public PlayerProfilesController(IPlayerProfileService playerProfileService)
    {
        _playerProfileService = playerProfileService;
    }

    [HttpGet]
    public async Task<ActionResult<List<PlayerProfileDto>>> GetAll()
    {
        var profiles = await _playerProfileService.GetAllAsync();
        return Ok(profiles);
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult<PlayerProfileDto>> GetByUserId(int userId)
    {
        var profile = await _playerProfileService.GetByUserIdAsync(userId);
        if (profile == null) return NotFound(new { message = "Profil bulunamadı." });
        return Ok(profile);
    }

    [HttpPost("me")]
    [Authorize]
    public async Task<ActionResult<PlayerProfileDto>> Upsert([FromBody] UpsertPlayerProfileDto dto)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null) return Unauthorized();

        var userId = int.Parse(userIdClaim);
        var profile = await _playerProfileService.UpsertAsync(userId, dto);
        return Ok(profile);
    }
}