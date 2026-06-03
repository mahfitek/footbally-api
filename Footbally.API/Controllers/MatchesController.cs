using Footbally.Application.DTOs.Match;
using Footbally.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Footbally.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MatchesController : ControllerBase
{
    private readonly IMatchService _matchService;

    public MatchesController(IMatchService matchService)
    {
        _matchService = matchService;
    }

    [HttpGet]
    public async Task<ActionResult<List<MatchResponseDto>>> GetAll()
    {
        var matches = await _matchService.GetAllAsync();
        return Ok(matches);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MatchResponseDto>> GetById(int id)
    {
        var match = await _matchService.GetByIdAsync(id);
        if (match == null) return NotFound(new { message = "Maç bulunamadı." });
        return Ok(match);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<MatchResponseDto>> Create([FromBody] CreateMatchDto dto)
    {
        var match = await _matchService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = match.Id }, match);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _matchService.DeleteAsync(id);
        if (!result) return NotFound(new { message = "Maç bulunamadı." });
        return NoContent();
    }

    [HttpPost("{id}/apply")]
    [Authorize]
    public async Task<ActionResult<MatchApplicationDto>> Apply(int id)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null) return Unauthorized();

        try
        {
            var userId = int.Parse(userIdClaim);
            var application = await _matchService.ApplyAsync(id, userId);
            return Ok(application);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpGet("{id}/applications")]
    [Authorize]
    public async Task<ActionResult<List<MatchApplicationDto>>> GetApplications(int id)
    {
        var applications = await _matchService.GetApplicationsAsync(id);
        return Ok(applications);
    }

    [HttpPut("{id}/applications/{applicationId}")]
    [Authorize]
    public async Task<ActionResult<MatchApplicationDto>> UpdateApplicationStatus(
        int id, int applicationId, [FromBody] UpdateApplicationStatusDto dto)
    {
        var result = await _matchService.UpdateApplicationStatusAsync(id, applicationId, dto.Status);
        if (result == null) return NotFound(new { message = "Başvuru bulunamadı." });
        return Ok(result);
    }
}