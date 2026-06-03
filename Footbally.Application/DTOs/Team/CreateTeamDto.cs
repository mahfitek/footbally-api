namespace Footbally.Application.DTOs.Team;

public class CreateTeamDto
{
    public string Name { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public int OwnerId { get; set; }
}