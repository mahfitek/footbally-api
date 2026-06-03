
namespace Footbally.Application.AI.DTOs.Output;

public class TrustScoreOutput
{
    public float Score { get; set; }
    public string? ScoreLabel { get; set; }
    public List<string> PositiveSignals { get; set; } = [];
    public List<string> NegativeSignals { get; set; } = [];
    public float ConfidenceScore { get; set; }
    public List<string> UsedDataFields { get; set; } = [];
    public List<string> MissingDataFields { get; set; } = [];
    public bool AdminReviewRequired { get; set; }
}