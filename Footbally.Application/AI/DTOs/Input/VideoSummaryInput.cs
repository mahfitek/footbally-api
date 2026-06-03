
namespace Footbally.Application.AI.DTOs.Input;

public class VideoSummaryInput
{
    public Guid PlayerId { get; set; }
    public string? VideoDescription { get; set; }
    public List<string> VideoTags { get; set; } = [];
    public string? AnalysisText { get; set; }
}