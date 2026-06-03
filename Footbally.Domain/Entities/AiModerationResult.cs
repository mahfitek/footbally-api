
namespace Footbally.Domain.Entities;

public class AiModerationResult
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid AiJobId { get; set; }
    public Guid TargetEntityId { get; set; }
    public string TargetEntityType { get; set; } = string.Empty;
    public string RiskLevel { get; set; } = "low";
    public string? FlagsJson { get; set; }
    public bool AdminReviewRequired { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public AiJob AiJob { get; set; } = null!;
}