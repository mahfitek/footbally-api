
using System.Text.Json;
using Footbally.Application.AI.DTOs.Input;
using Footbally.Application.Interfaces;
using Footbally.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Footbally.API.Controllers.AI;

[ApiController]
[Route("api/ai/admin")]
[Authorize(Roles = "Admin")]
public class AdminAiController : ControllerBase
{
    private readonly IAiJobRepository _jobRepo;

    public AdminAiController(IAiJobRepository jobRepo)
    {
        _jobRepo = jobRepo;
    }

    [HttpPost("moderation")]
    public async Task<IActionResult> TriggerModeration([FromBody] ModerationInput input)
    {
        var job = new AiJob
        {
            JobType = "Moderation",
            TargetEntityId = input.EntityId,
            TargetEntityType = input.EntityType,
            InputJson = JsonSerializer.Serialize(input),
            Status = "Pending"
        };

        await _jobRepo.CreateAsync(job);
        return Ok(new { jobId = job.Id, message = "Moderasyon analizi başlatıldı." });
    }

    [HttpPost("trust-score")]
    public async Task<IActionResult> TriggerTrustScore([FromBody] TrustScoreInput input)
    {
        var job = new AiJob
        {
            JobType = "TrustScore",
            TargetEntityId = input.PlayerId,
            TargetEntityType = "Player",
            InputJson = JsonSerializer.Serialize(input),
            Status = "Pending"
        };

        await _jobRepo.CreateAsync(job);
        return Ok(new { jobId = job.Id, message = "Güven skoru hesaplanıyor." });
    }

    [HttpGet("trust-score/{playerId}")]
    public async Task<IActionResult> GetTrustScore(Guid playerId)
    {
        var job = await _jobRepo.GetLatestByEntityAsync(playerId, "TrustScore");
        if (job == null) return NotFound(new { message = "Henüz güven skoru hesaplanmamış." });
        if (job.OutputJson == null) return Ok(new { status = job.Status, message = "Hesaplanıyor." });

        var output = JsonSerializer.Deserialize<object>(job.OutputJson);
        return Ok(new { status = job.Status, confidenceScore = job.ConfidenceScore, data = output });
    }

    [HttpGet("jobs")]
    public async Task<IActionResult> GetJobs(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string? status = null,
        [FromQuery] string? jobType = null)
    {
        var jobs = await _jobRepo.GetAllForAdminAsync(page, pageSize, status, jobType);
        var total = await _jobRepo.CountForAdminAsync(status, jobType);

        return Ok(new
        {
            total,
            page,
            pageSize,
            data = jobs.Select(j => new
            {
                j.Id,
                j.JobType,
                j.TargetEntityId,
                j.TargetEntityType,
                j.Status,
                j.ConfidenceScore,
                j.AdminReviewRequired,
                j.AdminReviewed,
                j.TokensUsed,
                j.ModelUsed,
                j.CreatedAt,
                j.CompletedAt
            })
        });
    }

    [HttpGet("jobs/{jobId}")]
    public async Task<IActionResult> GetJobDetail(Guid jobId)
    {
        var job = await _jobRepo.GetByIdAsync(jobId);
        if (job == null) return NotFound();

        object? input = null;
        object? output = null;

        if (!string.IsNullOrEmpty(job.InputJson))
            input = JsonSerializer.Deserialize<object>(job.InputJson);

        if (!string.IsNullOrEmpty(job.OutputJson))
            output = JsonSerializer.Deserialize<object>(job.OutputJson);

        return Ok(new
        {
            job.Id,
            job.JobType,
            job.TargetEntityId,
            job.TargetEntityType,
            job.Status,
            job.ConfidenceScore,
            job.AdminReviewRequired,
            job.AdminReviewed,
            job.AdminNote,
            job.ErrorMessage,
            job.TokensUsed,
            job.ModelUsed,
            job.CreatedAt,
            job.CompletedAt,
            Input = input,
            Output = output
        });
    }

    [HttpPatch("jobs/{jobId}/review")]
    public async Task<IActionResult> ReviewJob(Guid jobId, [FromBody] ReviewJobRequest request)
    {
        var job = await _jobRepo.GetByIdAsync(jobId);
        if (job == null) return NotFound();

        job.AdminReviewed = true;
        job.AdminNote = request.Note;
        job.Status = request.Approved ? "AdminApproved" : "AdminRejected";
        job.ReviewedByAdminId = request.AdminId;

        await _jobRepo.UpdateAsync(job);
        return Ok(new { message = "İnceleme kaydedildi." });
    }
    [HttpPost("fake-detection")]
    public async Task<IActionResult> TriggerFakeDetection([FromBody] FakeProfileDetectionInput input)
    {
        var job = new AiJob { JobType = "FakeProfileDetection", TargetEntityId = input.PlayerId, TargetEntityType = "Player", InputJson = JsonSerializer.Serialize(input), Status = "Pending" };
        await _jobRepo.CreateAsync(job);
        return Ok(new { jobId = job.Id, message = "Sahte profil analizi başlatıldı." });
    }

    [HttpPost("content-quality")]
    public async Task<IActionResult> TriggerContentQuality([FromBody] ContentQualityInput input)
    {
        var job = new AiJob { JobType = "ContentQuality", TargetEntityId = input.PlayerId, TargetEntityType = "Player", InputJson = JsonSerializer.Serialize(input), Status = "Pending" };
        await _jobRepo.CreateAsync(job);
        return Ok(new { jobId = job.Id, message = "İçerik kalitesi analiz ediliyor." });
    }

    [HttpPost("support-ticket")]
    public async Task<IActionResult> TriggerSupportTicket([FromBody] SupportTicketInput input)
    {
        var job = new AiJob { JobType = "SupportTicket", TargetEntityId = input.TicketId, TargetEntityType = "SupportTicket", InputJson = JsonSerializer.Serialize(input), Status = "Pending" };
        await _jobRepo.CreateAsync(job);
        return Ok(new { jobId = job.Id, message = "Destek talebi sınıflandırılıyor." });
    }
}

public class ReviewJobRequest
{
    public bool Approved { get; set; }
    public string? Note { get; set; }
    public Guid AdminId { get; set; }
}