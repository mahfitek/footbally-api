namespace Footbally.Domain.Entities;

public class TeamProfile
{
    public int Id { get; set; }
    public int TeamId { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? LogoUrl { get; set; }
    public string Level { get; set; } = "Amateur";
    public int FoundedYear { get; set; }
    public string PreferredFormat { get; set; } = string.Empty;
    public string MatchDays { get; set; } = string.Empty;
    public string NeededPositions { get; set; } = string.Empty;
    public bool IsLookingForPlayers { get; set; } = true;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public Team Team { get; set; } = null!;
}