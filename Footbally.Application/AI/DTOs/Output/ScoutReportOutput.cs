
namespace Footbally.Application.AI.DTOs.Output;

public class ScoutReportOutput
{
    public string? TechnicalSummary { get; set; }
    public string? PhysicalSummary { get; set; }
    public string? TacticalSummary { get; set; }
    public string? MentalSummary { get; set; }
    public string? ProfileReliability { get; set; }
    public string? Verdict { get; set; }
    public string? VerdictReason { get; set; }
    public float ConfidenceScore { get; set; }
    public List<string> UsedDataFields { get; set; } = [];
    public List<string> MissingDataFields { get; set; } = [];
    public bool AdminReviewRequired { get; set; }
}