namespace Footbally.Domain.Entities;

public class MatchApplication
{
    public int Id { get; set; }
    public int MatchId { get; set; }
    public int UserId { get; set; }
    public string Status { get; set; } = "Pending";
    public DateTime AppliedAt { get; set; } = DateTime.UtcNow;

    public Match Match { get; set; } = null!;
    public User User { get; set; } = null!;
}