
namespace Footbally.Domain.Entities;

public class AiTrustScore
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid PlayerId { get; set; }
    public Guid AiJobId { get; set; }
    public float Score { get; set; }
    public string? ScoreLabel { get; set; }
    public string? SignalsJson { get; set; }
    public float ConfidenceScore { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public AiJob AiJob { get; set; } = null!;
}