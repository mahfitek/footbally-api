
namespace Footbally.Application.AI.DTOs.Input;

public class MatchRecommendationInput
{
    public Guid PlayerId { get; set; }
    public string? City { get; set; }
    public string? District { get; set; }
    public string? Position { get; set; }
    public string? Level { get; set; }
    public float? TrustScore { get; set; }
    public List<AvailableMatchInfo> AvailableMatches { get; set; } = [];
}

public class AvailableMatchInfo
{
    public Guid MatchId { get; set; }
    public string? Location { get; set; }
    public string? City { get; set; }
    public string? District { get; set; }
    public string? NeededPosition { get; set; }
    public string? Level { get; set; }
    public DateTime MatchDate { get; set; }
}