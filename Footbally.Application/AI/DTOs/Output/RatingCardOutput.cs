
namespace Footbally.Application.AI.DTOs.Output;

public class RatingCardOutput
{
    public int OverallRating { get; set; }
    public int Pace { get; set; }
    public int Shooting { get; set; }
    public int Passing { get; set; }
    public int Defending { get; set; }
    public int Physical { get; set; }
    public int Technique { get; set; }
    public int GameIntelligence { get; set; }
    public string? CardTier { get; set; }
    public string? RatingNote { get; set; }
    public float ConfidenceScore { get; set; }
    public List<string> UsedDataFields { get; set; } = [];
    public List<string> MissingDataFields { get; set; } = [];
    public bool AdminReviewRequired { get; set; }
}