namespace Footbally.Application.DTOs.Match;

public class CreateMatchDto
{
    public int HomeTeamId { get; set; }
    public string Location { get; set; } = string.Empty;
    public DateTime MatchDate { get; set; }
    public int PlayerQuota { get; set; } = 10;
    public string Description { get; set; } = string.Empty;
}