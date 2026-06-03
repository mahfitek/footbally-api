namespace Footbally.Application.DTOs.Player;

public class UpsertPlayerProfileDto
{
    public string Position { get; set; } = string.Empty;
    public string Foot { get; set; } = string.Empty;
    public int? Height { get; set; }
    public int? Weight { get; set; }
    public int? Age { get; set; }
    public string City { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
    public bool IsAvailable { get; set; } = true;
}