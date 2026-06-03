using System.Text.Json;
using Footbally.Application.AI.DTOs.Input;
using Footbally.Application.Interfaces;
using Footbally.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Footbally.API.Controllers.AI;

[ApiController]
[Route("api/ai/scout")]
[Authorize]
public class ScoutAiController : ControllerBase
{
    private readonly IAiJobRepository _jobRepo;

    public ScoutAiController(IAiJobRepository jobRepo)
    {
        _jobRepo = jobRepo;
    }

    [HttpPost("report")]
    public async Task<IActionResult> TriggerScoutReport([FromBody] ScoutReportInput input)
    {
        var job = new AiJob { JobType = "ScoutReport", TargetEntityId = input.PlayerId, TargetEntityType = "Player", InputJson = JsonSerializer.Serialize(input), Status = "Pending" };
        await _jobRepo.CreateAsync(job);
        return Ok(new { jobId = job.Id, message = "Scout raporu hazırlanıyor." });
    }

    [HttpGet("report/{playerId}")]
    public async Task<IActionResult> GetScoutReport(Guid playerId)
    {
        var job = await _jobRepo.GetLatestByEntityAsync(playerId, "ScoutReport");
        if (job == null) return NotFound(new { message = "Henüz rapor oluşturulmamış." });
        if (job.OutputJson == null) return Ok(new { status = job.Status, message = "Rapor hazırlanıyor." });
        return Ok(new { status = job.Status, confidenceScore = job.ConfidenceScore, data = JsonSerializer.Deserialize<object>(job.OutputJson) });
    }

    [HttpPost("player-discovery")]
    public async Task<IActionResult> TriggerPlayerDiscovery([FromBody] PlayerDiscoveryInput input)
    {
        var job = new AiJob { JobType = "PlayerDiscovery", TargetEntityId = input.ScoutId, TargetEntityType = "Scout", InputJson = JsonSerializer.Serialize(input), Status = "Pending" };
        await _jobRepo.CreateAsync(job);
        return Ok(new { jobId = job.Id, message = "Oyuncu keşfi başlatıldı." });
    }

    [HttpPost("video-summary")]
    public async Task<IActionResult> TriggerVideoSummary([FromBody] VideoSummaryInput input)
    {
        var job = new AiJob { JobType = "VideoSummary", TargetEntityId = input.PlayerId, TargetEntityType = "Player", InputJson = JsonSerializer.Serialize(input), Status = "Pending" };
        await _jobRepo.CreateAsync(job);
        return Ok(new { jobId = job.Id, message = "Video özeti hazırlanıyor." });
    }

    [HttpPost("comparison")]
    public async Task<IActionResult> TriggerComparison([FromBody] PlayerComparisonInput input)
    {
        var job = new AiJob { JobType = "PlayerComparison", TargetEntityId = input.ScoutId, TargetEntityType = "Scout", InputJson = JsonSerializer.Serialize(input), Status = "Pending" };
        await _jobRepo.CreateAsync(job);
        return Ok(new { jobId = job.Id, message = "Oyuncu karşılaştırması başlatıldı." });
    }

    [HttpPost("message")]
    public async Task<IActionResult> TriggerScoutMessage([FromBody] ScoutMessageInput input)
    {
        var job = new AiJob { JobType = "ScoutMessage", TargetEntityId = input.ScoutId, TargetEntityType = "Scout", InputJson = JsonSerializer.Serialize(input), Status = "Pending" };
        await _jobRepo.CreateAsync(job);
        return Ok(new { jobId = job.Id, message = "Mesaj hazırlanıyor." });
    }

    [HttpGet("job/{jobId}")]
    public async Task<IActionResult> GetJobResult(Guid jobId)
    {
        var job = await _jobRepo.GetByIdAsync(jobId);
        if (job == null) return NotFound();
        if (job.OutputJson == null) return Ok(new { status = job.Status, message = "İşlem devam ediyor." });
        return Ok(new { status = job.Status, confidenceScore = job.ConfidenceScore, data = JsonSerializer.Deserialize<object>(job.OutputJson) });
    }
}