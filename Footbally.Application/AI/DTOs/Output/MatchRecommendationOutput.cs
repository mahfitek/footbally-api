
namespace Footbally.Application.AI.DTOs.Output;

public class MatchRecommendationOutput
{
    public List<MatchRecommendationItem> Recommendations { get; set; } = [];
    public float ConfidenceScore { get; set; }
    public List<string> UsedDataFields { get; set; } = [];
    public List<string> MissingDataFields { get; set; } = [];
    public bool AdminReviewRequired { get; set; }
}

public class MatchRecommendationItem
{
    public Guid MatchId { get; set; }
    public string? Reason { get; set; }
    public int CompatibilityScore { get; set; }
}