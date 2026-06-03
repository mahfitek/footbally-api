using Footbally.Application.DTOs.Team;
using Footbally.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Footbally.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamsController : ControllerBase
{
    private readonly ITeamService _teamService;

    public TeamsController(ITeamService teamService)
    {
        _teamService = teamService;
    }

    [HttpGet]
    public async Task<ActionResult<List<TeamResponseDto>>> GetAll()
    {
        var teams = await _teamService.GetAllAsync();
        return Ok(teams);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TeamResponseDto>> GetById(int id)
    {
        var team = await _teamService.GetByIdAsync(id);
        if (team == null) return NotFound(new { message = "Takım bulunamadı." });
        return Ok(team);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<TeamResponseDto>> Create([FromBody] CreateTeamDto dto)
    {
        var team = await _teamService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = team.Id }, team);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _teamService.DeleteAsync(id);
        if (!result) return NotFound(new { message = "Takım bulunamadı." });
        return NoContent();
    }

    [HttpGet("{id}/profile")]
    public async Task<ActionResult<TeamProfileResponseDto>> GetProfile(int id)
    {
        var profile = await _teamService.GetProfileByTeamIdAsync(id);
        if (profile == null) return NotFound(new { message = "Takım profili bulunamadı." });
        return Ok(profile);
    }

    [HttpPost("{id}/profile")]
    [Authorize]
    public async Task<ActionResult<TeamProfileResponseDto>> UpsertProfile(
        int id, [FromBody] UpsertTeamProfileDto dto)
    {
        try
        {
            var profile = await _teamService.UpsertProfileAsync(id, dto);
            return Ok(profile);
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}