namespace Footbally.Application.DTOs.Match;

public class MatchResponseDto
{
    public int Id { get; set; }
    public int HomeTeamId { get; set; }
    public string HomeTeamName { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateTime MatchDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public int PlayerQuota { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}