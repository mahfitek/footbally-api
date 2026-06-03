
namespace Footbally.Application.AI.DTOs.Input;

public class PlayerComparisonInput
{
    public Guid ScoutId { get; set; }
    public List<PlayerCandidateInfo> Players { get; set; } = [];
}