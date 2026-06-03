
namespace Footbally.Domain.Entities;

public class AiScoutReport
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ScoutId { get; set; }
    public Guid PlayerId { get; set; }
    public Guid AiJobId { get; set; }
    public string? ReportJson { get; set; }
    public string? Verdict { get; set; }
    public float ConfidenceScore { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public AiJob AiJob { get; set; } = null!;
}