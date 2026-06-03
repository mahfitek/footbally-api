
namespace Footbally.Application.AI.DTOs.Output;

public class PlayerDiscoveryOutput
{
    public List<PlayerDiscoveryItem> RankedPlayers { get; set; } = [];
    public float ConfidenceScore { get; set; }
    public List<string> UsedDataFields { get; set; } = [];
    public List<string> MissingDataFields { get; set; } = [];
    public bool AdminReviewRequired { get; set; }
}

public class PlayerDiscoveryItem
{
    public Guid PlayerId { get; set; }
    public int CompatibilityScore { get; set; }
    public string? RecommendReason { get; set; }
    public string? RiskNote { get; set; }
    public string? MissingInfo { get; set; }
}