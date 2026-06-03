using System.Text.Json;
using Footbally.Application.AI.DTOs.Input;
using Footbally.Application.Interfaces;
using Footbally.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Footbally.API.Controllers.AI;

[ApiController]
[Route("api/ai/player")]
[Authorize]
public class PlayerAiController : ControllerBase
{
    private readonly IAiJobRepository _jobRepo;

    public PlayerAiController(IAiJobRepository jobRepo)
    {
        _jobRepo = jobRepo;
    }

    [HttpPost("profile-coach")]
    public async Task<IActionResult> TriggerProfileCoach([FromBody] ProfileCoachInput input)
    {
        var job = new AiJob { JobType = "ProfileCoach", TargetEntityId = input.PlayerId, TargetEntityType = "Player", InputJson = JsonSerializer.Serialize(input), Status = "Pending" };
        await _jobRepo.CreateAsync(job);
        return Ok(new { jobId = job.Id, message = "AI analizi başlatıldı." });
    }

    [HttpGet("profile-coach/{playerId}")]
    public async Task<IActionResult> GetProfileCoach(Guid playerId)
    {
        var job = await _jobRepo.GetLatestByEntityAsync(playerId, "ProfileCoach");
        if (job == null) return NotFound(new { message = "Henüz analiz yapılmamış." });
        if (job.OutputJson == null) return Ok(new { status = job.Status, message = "Analiz devam ediyor." });
        return Ok(new { status = job.Status, confidenceScore = job.ConfidenceScore, data = JsonSerializer.Deserialize<object>(job.OutputJson) });
    }

    [HttpPost("rating")]
    public async Task<IActionResult> TriggerRating([FromBody] RatingCardInput input)
    {
        var job = new AiJob { JobType = "RatingCard", TargetEntityId = input.PlayerId, TargetEntityType = "Player", InputJson = JsonSerializer.Serialize(input), Status = "Pending" };
        await _jobRepo.CreateAsync(job);
        return Ok(new { jobId = job.Id, message = "Rating hesaplanıyor." });
    }

    [HttpGet("rating/{playerId}")]
    public async Task<IActionResult> GetRating(Guid playerId)
    {
        var job = await _jobRepo.GetLatestByEntityAsync(playerId, "RatingCard");
        if (job == null) return NotFound(new { message = "Henüz rating hesaplanmamış." });
        if (job.OutputJson == null) return Ok(new { status = job.Status, message = "Rating hesaplanıyor." });
        return Ok(new { status = job.Status, confidenceScore = job.ConfidenceScore, data = JsonSerializer.Deserialize<object>(job.OutputJson) });
    }

    [HttpPost("analysis")]
    public async Task<IActionResult> TriggerAnalysis([FromBody] PlayerAnalysisInput input)
    {
        var job = new AiJob { JobType = "PlayerAnalysis", TargetEntityId = input.PlayerId, TargetEntityType = "Player", InputJson = JsonSerializer.Serialize(input), Status = "Pending" };
        await _jobRepo.CreateAsync(job);
        return Ok(new { jobId = job.Id, message = "Oyuncu analizi başlatıldı." });
    }

    [HttpGet("analysis/{playerId}")]
    public async Task<IActionResult> GetAnalysis(Guid playerId)
    {
        var job = await _jobRepo.GetLatestByEntityAsync(playerId, "PlayerAnalysis");
        if (job == null) return NotFound(new { message = "Henüz analiz yapılmamış." });
        if (job.OutputJson == null) return Ok(new { status = job.Status, message = "Analiz devam ediyor." });
        return Ok(new { status = job.Status, confidenceScore = job.ConfidenceScore, data = JsonSerializer.Deserialize<object>(job.OutputJson) });
    }

    [HttpPost("career-coach")]
    public async Task<IActionResult> TriggerCareerCoach([FromBody] CareerCoachInput input)
    {
        var job = new AiJob { JobType = "CareerCoach", TargetEntityId = input.PlayerId, TargetEntityType = "Player", InputJson = JsonSerializer.Serialize(input), Status = "Pending" };
        await _jobRepo.CreateAsync(job);
        return Ok(new { jobId = job.Id, message = "Kariyer planı hazırlanıyor." });
    }

    [HttpGet("career-coach/{playerId}")]
    public async Task<IActionResult> GetCareerCoach(Guid playerId)
    {
        var job = await _jobRepo.GetLatestByEntityAsync(playerId, "CareerCoach");
        if (job == null) return NotFound(new { message = "Henüz kariyer planı oluşturulmamış." });
        if (job.OutputJson == null) return Ok(new { status = job.Status, message = "Plan hazırlanıyor." });
        return Ok(new { status = job.Status, confidenceScore = job.ConfidenceScore, data = JsonSerializer.Deserialize<object>(job.OutputJson) });
    }

    [HttpPost("football-cv")]
    public async Task<IActionResult> TriggerFootballCv([FromBody] FootballCvInput input)
    {
        var job = new AiJob { JobType = "FootballCv", TargetEntityId = input.PlayerId, TargetEntityType = "Player", InputJson = JsonSerializer.Serialize(input), Status = "Pending" };
        await _jobRepo.CreateAsync(job);
        return Ok(new { jobId = job.Id, message = "Futbol CV'si hazırlanıyor." });
    }

    [HttpGet("football-cv/{playerId}")]
    public async Task<IActionResult> GetFootballCv(Guid playerId)
    {
        var job = await _jobRepo.GetLatestByEntityAsync(playerId, "FootballCv");
        if (job == null) return NotFound(new { message = "Henüz CV oluşturulmamış." });
        if (job.OutputJson == null) return Ok(new { status = job.Status, message = "CV hazırlanıyor." });
        return Ok(new { status = job.Status, confidenceScore = job.ConfidenceScore, data = JsonSerializer.Deserialize<object>(job.OutputJson) });
    }

    [HttpPost("match-recommendation")]
    public async Task<IActionResult> TriggerMatchRecommendation([FromBody] MatchRecommendationInput input)
    {
        var job = new AiJob { JobType = "MatchRecommendation", TargetEntityId = input.PlayerId, TargetEntityType = "Player", InputJson = JsonSerializer.Serialize(input), Status = "Pending" };
        await _jobRepo.CreateAsync(job);
        return Ok(new { jobId = job.Id, message = "Maç önerileri hazırlanıyor." });
    }

    [HttpGet("match-recommendation/{playerId}")]
    public async Task<IActionResult> GetMatchRecommendation(Guid playerId)
    {
        var job = await _jobRepo.GetLatestByEntityAsync(playerId, "MatchRecommendation");
        if (job == null) return NotFound(new { message = "Henüz maç önerisi oluşturulmamış." });
        if (job.OutputJson == null) return Ok(new { status = job.Status, message = "Öneriler hazırlanıyor." });
        return Ok(new { status = job.Status, confidenceScore = job.ConfidenceScore, data = JsonSerializer.Deserialize<object>(job.OutputJson) });
    }
}