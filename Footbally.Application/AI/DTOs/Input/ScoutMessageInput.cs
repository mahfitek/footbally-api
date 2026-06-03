
namespace Footbally.Application.AI.DTOs.Input;

public class ScoutMessageInput
{
    public Guid ScoutId { get; set; }
    public string? ScoutName { get; set; }
    public string? ClubOrOrganization { get; set; }
    public Guid PlayerId { get; set; }
    public string? PlayerName { get; set; }
    public string? PlayerPosition { get; set; }
    public string? Purpose { get; set; }
}