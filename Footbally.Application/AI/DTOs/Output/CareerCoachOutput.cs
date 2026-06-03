
namespace Footbally.Application.AI.DTOs.Output;

public class CareerCoachOutput
{
    public string? SevenDayPlan { get; set; }
    public string? ThirtyDayPlan { get; set; }
    public string? NinetyDayPlan { get; set; }
    public string? TrainingAdvice { get; set; }
    public float ConfidenceScore { get; set; }
    public List<string> UsedDataFields { get; set; } = [];
    public List<string> MissingDataFields { get; set; } = [];
    public bool AdminReviewRequired { get; set; }
}