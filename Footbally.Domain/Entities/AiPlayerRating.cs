
namespace Footbally.Domain.Entities;

public class AiPlayerRating
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid PlayerId { get; set; }
    public Guid AiJobId { get; set; }
    public int OverallRating { get; set; }
    public int Pace { get; set; }
    public int Shooting { get; set; }
    public int Passing { get; set; }
    public int Defending { get; set; }
    public int Physical { get; set; }
    public int Technique { get; set; }
    public int GameIntelligence { get; set; }
    public string? CardTier { get; set; }
    public float ConfidenceScore { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public AiJob AiJob { get; set; } = null!;
}