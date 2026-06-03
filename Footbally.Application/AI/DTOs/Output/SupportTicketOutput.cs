
namespace Footbally.Application.AI.DTOs.Output;

public class SupportTicketOutput
{
    public string? Category { get; set; }
    public string? Priority { get; set; }
    public string? AdminDraftReply { get; set; }
    public float ConfidenceScore { get; set; }
    public List<string> UsedDataFields { get; set; } = [];
    public List<string> MissingDataFields { get; set; } = [];
    public bool AdminReviewRequired { get; set; }
}