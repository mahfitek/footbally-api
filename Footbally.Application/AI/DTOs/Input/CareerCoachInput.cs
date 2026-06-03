
namespace Footbally.Application.AI.DTOs.Input;

public class CareerCoachInput
{
    public Guid PlayerId { get; set; }
    public string? Position { get; set; }
    public string? Level { get; set; }
    public int? Age { get; set; }
    public string? About { get; set; }
    public List<string> TeamHistory { get; set; } = [];
    public bool HasVideo { get; set; }
    public bool HasCv { get; set; }
    public int ProfileCompletionPercent { get; set; }
    public bool IsPremium { get; set; }
}