namespace Footbally.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string Role { get; set; } = "Player";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;

    public PlayerProfile? PlayerProfile { get; set; }
    public ICollection<Team> OwnedTeams { get; set; } = new List<Team>();
    public ICollection<MatchApplication> MatchApplications { get; set; } = new List<MatchApplication>();
}