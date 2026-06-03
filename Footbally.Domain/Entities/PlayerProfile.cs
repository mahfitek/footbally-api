namespace Footbally.Domain.Entities;

public class PlayerProfile
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Position { get; set; } = string.Empty;
    public string Foot { get; set; } = string.Empty;
    public int? Height { get; set; }
    public int? Weight { get; set; }
    public int? Age { get; set; }
    public string City { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public string? AvatarUrl { get; set; }
    public bool IsAvailable { get; set; } = true;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = null!;
}