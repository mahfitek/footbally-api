
namespace Footbally.Application.AI.DTOs.Input;

public class PlayerAnalysisInput
{
    public Guid PlayerId { get; set; }
    public string? FullName { get; set; }
    public string? Position { get; set; }
    public string? Level { get; set; }
    public int? Age { get; set; }
    public int? Height { get; set; }
    public int? Weight { get; set; }
    public string? PreferredFoot { get; set; }
    public string? About { get; set; }
    public List<string> TeamHistory { get; set; } = [];
    public bool HasVideo { get; set; }
    public bool HasCv { get; set; }
    public string? City { get; set; }
    public int ProfileCompletionPercent { get; set; }
}