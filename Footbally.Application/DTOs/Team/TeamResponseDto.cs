namespace Footbally.Application.DTOs.Team;

public class TeamResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public int OwnerId { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
}