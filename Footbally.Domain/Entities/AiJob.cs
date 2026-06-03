namespace Footbally.Domain.Entities;

public class AiJob
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string JobType { get; set; } = string.Empty;
    public Guid? TargetEntityId { get; set; }
    public string? TargetEntityType { get; set; }
    public string Status { get; set; } = "Pending";
    public string InputJson { get; set; } = string.Empty;
    public string? OutputJson { get; set; }
    public float? ConfidenceScore { get; set; }
    public bool AdminReviewRequired { get; set; }
    public bool AdminReviewed { get; set; }
    public Guid? ReviewedByAdminId { get; set; }
    public string? AdminNote { get; set; }
    public string? ErrorMessage { get; set; }
    public int TokensUsed { get; set; }
    public string? ModelUsed { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }
}