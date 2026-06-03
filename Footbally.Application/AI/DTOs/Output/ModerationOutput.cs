
namespace Footbally.Application.AI.DTOs.Output;

public class ModerationOutput
{
    public string RiskLevel { get; set; } = "low";
    public List<string> Flags { get; set; } = [];
    public string? Summary { get; set; }
    public float ConfidenceScore { get; set; }
    public List<string> UsedDataFields { get; set; } = [];
    public List<string> MissingDataFields { get; set; } = [];
    public bool AdminReviewRequired { get; set; }
}