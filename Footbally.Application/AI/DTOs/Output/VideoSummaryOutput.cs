
namespace Footbally.Application.AI.DTOs.Output;

public class VideoSummaryOutput
{
    public string? PerformanceSummary { get; set; }
    public bool IsRealVideoAnalysis { get; set; }
    public string? DataSourceNote { get; set; }
    public float ConfidenceScore { get; set; }
    public List<string> UsedDataFields { get; set; } = [];
    public List<string> MissingDataFields { get; set; } = [];
    public bool AdminReviewRequired { get; set; }
}