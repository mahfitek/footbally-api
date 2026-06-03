
namespace Footbally.Application.AI.DTOs.Input;

public class ContentQualityInput
{
    public Guid PlayerId { get; set; }
    public bool HasPhoto { get; set; }
    public bool HasVideo { get; set; }
    public bool HasCv { get; set; }
    public string? Position { get; set; }
    public int? Height { get; set; }
    public int? Weight { get; set; }
    public List<string> TeamHistory { get; set; } = [];
    public string? About { get; set; }
    public int ProfileCompletionPercent { get; set; }
}