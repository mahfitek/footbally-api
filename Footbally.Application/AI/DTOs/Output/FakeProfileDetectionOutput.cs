
namespace Footbally.Application.AI.DTOs.Output;

public class FakeProfileDetectionOutput
{
    public string? RiskLevel { get; set; }
    public List<string> RiskSignals { get; set; } = [];
    public string? Summary { get; set; }
    public float ConfidenceScore { get; set; }
    public List<string> UsedDataFields { get; set; } = [];
    public List<string> MissingDataFields { get; set; } = [];
    public bool AdminReviewRequired { get; set; }
}