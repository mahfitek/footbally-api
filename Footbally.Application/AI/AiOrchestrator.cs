using Footbally.Application.AI.Agents;
using Footbally.Application.Interfaces;
using Footbally.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Footbally.Application.AI;

public class AiOrchestrator
{
    private readonly IAiJobRepository _jobRepo;
    private readonly IServiceProvider _services;

    public AiOrchestrator(IAiJobRepository jobRepo, IServiceProvider services)
    {
        _jobRepo = jobRepo;
        _services = services;
    }

    public async Task ProcessAsync(AiJob job, CancellationToken cancellationToken = default)
    {
        job.Status = "Processing";
        await _jobRepo.UpdateAsync(job, cancellationToken);

        try
        {
            var agent = ResolveAgent(job.JobType);
            var result = await agent.RunAsync(job.InputJson, cancellationToken);

            if (!result.Success)
            {
                job.Status = "Failed";
                job.ErrorMessage = result.ErrorMessage;
            }
            else
            {
                job.OutputJson = result.OutputJson;
                job.ConfidenceScore = result.ConfidenceScore;
                job.AdminReviewRequired = result.AdminReviewRequired;
                job.TokensUsed = result.TokensUsed;
                job.ModelUsed = result.ModelUsed;
                job.CompletedAt = DateTime.UtcNow;
                job.Status = result.AdminReviewRequired ? "AwaitingAdminReview" : "Completed";
            }
        }
        catch (Exception ex)
        {
            job.Status = "Failed";
            job.ErrorMessage = ex.Message;
        }

        await _jobRepo.UpdateAsync(job, cancellationToken);
    }

    private IAiAgent ResolveAgent(string jobType)
    {
        return jobType switch
        {
            "ProfileCoach" => _services.GetRequiredService<ProfileCoachAgent>(),
            "RatingCard" => _services.GetRequiredService<RatingCardAgent>(),
            "Moderation" => _services.GetRequiredService<ModerationAgent>(),
            "TrustScore" => _services.GetRequiredService<TrustScoreAgent>(),
            "ScoutReport" => _services.GetRequiredService<ScoutReportAgent>(),
            "PlayerAnalysis" => _services.GetRequiredService<PlayerAnalysisAgent>(),
            "CareerCoach" => _services.GetRequiredService<CareerCoachAgent>(),
            "FootballCv" => _services.GetRequiredService<FootballCvAgent>(),
            "MatchRecommendation" => _services.GetRequiredService<MatchRecommendationAgent>(),
            "PlayerDiscovery" => _services.GetRequiredService<PlayerDiscoveryAgent>(),
            "VideoSummary" => _services.GetRequiredService<VideoSummaryAgent>(),
            "PlayerComparison" => _services.GetRequiredService<PlayerComparisonAgent>(),
            "ScoutMessage" => _services.GetRequiredService<ScoutMessageAgent>(),
            "FakeProfileDetection" => _services.GetRequiredService<FakeProfileDetectionAgent>(),
            "ContentQuality" => _services.GetRequiredService<ContentQualityAgent>(),
            "SupportTicket" => _services.GetRequiredService<SupportTicketAgent>(),
            _ => throw new NotSupportedException($"Agent bulunamadı: {jobType}")
        };
    }
}