namespace Footbally.Application.DTOs.Match;

public class MatchApplicationDto
{
    public int Id { get; set; }
    public int MatchId { get; set; }
    public int UserId { get; set; }
    public string UserFullName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime AppliedAt { get; set; }
}