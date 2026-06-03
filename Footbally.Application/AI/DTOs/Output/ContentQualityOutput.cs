
namespace Footbally.Application.AI.DTOs.Output;

public class ContentQualityOutput
{
    public int QualityScore { get; set; }
    public string? QualityLabel { get; set; }
    public List<string> ImprovementSuggestions { get; set; } = [];
    public float ConfidenceScore { get; set; }
    public List<string> UsedDataFields { get; set; } = [];
    public List<string> MissingDataFields { get; set; } = [];
    public bool AdminReviewRequired { get; set; }
}