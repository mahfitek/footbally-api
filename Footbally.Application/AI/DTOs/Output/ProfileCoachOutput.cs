
namespace Footbally.Application.AI.DTOs.Output;

public class ProfileCoachOutput
{
    public List<string> MissingFields { get; set; } = [];
    public List<string> Suggestions { get; set; } = [];
    public string? ScoutVisibilityTip { get; set; }
    public bool PremiumSuggested { get; set; }
    public string? PremiumReason { get; set; }
    public float ConfidenceScore { get; set; }
    public List<string> UsedDataFields { get; set; } = [];
    public List<string> MissingDataFields { get; set; } = [];
    public bool AdminReviewRequired { get; set; }
}