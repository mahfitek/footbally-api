using Footbally.Application.DTOs;
using Footbally.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Footbally.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SupportController : ControllerBase
{
    private readonly ISupportService _supportService;

    public SupportController(ISupportService supportService)
    {
        _supportService = supportService;
    }

    [HttpPost("ask")]
    public async Task<ActionResult<SupportResponseDto>> Ask([FromBody] SupportRequestDto request)
    {
        if (string.IsNullOrWhiteSpace(request.Question))
            return BadRequest(new { message = "Soru boş olamaz." });

        var answer = await _supportService.AskAsync(request.Question);
        return Ok(new SupportResponseDto { Answer = answer });
    }
}