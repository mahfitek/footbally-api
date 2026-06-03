
namespace Footbally.Application.AI.DTOs.Input;

public class FootballCvInput
{
    public Guid PlayerId { get; set; }
    public string? FullName { get; set; }
    public int? Age { get; set; }
    public int? Height { get; set; }
    public int? Weight { get; set; }
    public string? Position { get; set; }
    public string? PreferredFoot { get; set; }
    public string? Level { get; set; }
    public string? City { get; set; }
    public string? About { get; set; }
    public List<string> TeamHistory { get; set; } = [];
    public bool HasVideo { get; set; }
    public bool FreeAgent { get; set; }
}