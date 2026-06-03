namespace Footbally.Application.DTOs.Team;

public class TeamProfileResponseDto
{
    public int Id { get; set; }
    public int TeamId { get; set; }
    public string TeamName { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? LogoUrl { get; set; }
    public string Level { get; set; } = string.Empty;
    public int FoundedYear { get; set; }
    public string PreferredFormat { get; set; } = string.Empty;
    public string MatchDays { get; set; } = string.Empty;
    public string NeededPositions { get; set; } = string.Empty;
    public bool IsLookingForPlayers { get; set; }
    public DateTime UpdatedAt { get; set; }
}