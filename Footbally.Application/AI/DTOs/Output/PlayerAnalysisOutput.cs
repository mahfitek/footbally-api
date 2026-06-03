
namespace Footbally.Application.AI.DTOs.Output;

public class PlayerAnalysisOutput
{
    public string? Strengths { get; set; }
    public string? Weaknesses { get; set; }
    public string? DevelopmentAreas { get; set; }
    public string? PlayingStyle { get; set; }
    public string? ScoutSummary { get; set; }
    public float ConfidenceScore { get; set; }
    public List<string> UsedDataFields { get; set; } = [];
    public List<string> MissingDataFields { get; set; } = [];
    public bool AdminReviewRequired { get; set; }
}