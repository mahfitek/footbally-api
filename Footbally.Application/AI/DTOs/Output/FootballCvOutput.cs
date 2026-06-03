
namespace Footbally.Application.AI.DTOs.Output;

public class FootballCvOutput
{
    public string? CvText { get; set; }
    public string? PdfReadyText { get; set; }
    public float ConfidenceScore { get; set; }
    public List<string> UsedDataFields { get; set; } = [];
    public List<string> MissingDataFields { get; set; } = [];
    public bool AdminReviewRequired { get; set; }
}