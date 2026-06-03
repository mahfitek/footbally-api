
namespace Footbally.Application.AI.DTOs.Input;

public class TrustScoreInput
{
    public Guid PlayerId { get; set; }
    public int ProfileCompletionPercent { get; set; }
    public bool IsVerified { get; set; }
    public int ComplaintCount { get; set; }
    public int NoShowCount { get; set; }
    public int PositiveReviewCount { get; set; }
    public int PaymentIssueCount { get; set; }
    public int AdminWarningCount { get; set; }
    public DateTime AccountCreatedAt { get; set; }
    public int MatchCount { get; set; }
}