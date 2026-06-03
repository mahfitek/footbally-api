
namespace Footbally.Application.AI.DTOs.Output;

public class PlayerComparisonOutput
{
    public Guid? RecommendedPlayerId { get; set; }
    public string? RecommendationReason { get; set; }
    public List<PlayerComparisonDetail> Details { get; set; } = [];
    public float ConfidenceScore { get; set; }
    public List<string> UsedDataFields { get; set; } = [];
    public List<string> MissingDataFields { get; set; } = [];
    public bool AdminReviewRequired { get; set; }
}

public class PlayerComparisonDetail
{
    public Guid PlayerId { get; set; }
    public string? Summary { get; set; }
    public string? Strengths { get; set; }
    public string? Weaknesses { get; set; }
}