namespace Footbally.Domain.Entities;

public class Match
{
    public int Id { get; set; }
    public int HomeTeamId { get; set; }
    public int? AwayTeamId { get; set; }
    public string Location { get; set; } = string.Empty;
    public DateTime MatchDate { get; set; }
    public string Status { get; set; } = "Open";
    public int PlayerQuota { get; set; } = 10;
    public string Description { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Team HomeTeam { get; set; } = null!;
    public Team? AwayTeam { get; set; }
    public ICollection<MatchApplication> Applications { get; set; } = new List<MatchApplication>();
}