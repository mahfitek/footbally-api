
namespace Footbally.Application.AI.DTOs.Input;

public class PlayerDiscoveryInput
{
    public Guid ScoutId { get; set; }
    public string? Position { get; set; }
    public string? City { get; set; }
    public int? MinAge { get; set; }
    public int? MaxAge { get; set; }
    public string? Level { get; set; }
    public string? PreferredFoot { get; set; }
    public bool RequireVideo { get; set; }
    public bool RequireCv { get; set; }
    public List<PlayerCandidateInfo> Candidates { get; set; } = [];
}

public class PlayerCandidateInfo
{
    public Guid PlayerId { get; set; }
    public string? FullName { get; set; }
    public string? Position { get; set; }
    public int? Age { get; set; }
    public string? City { get; set; }
    public string? Level { get; set; }
    public string? PreferredFoot { get; set; }
    public bool HasVideo { get; set; }
    public bool HasCv { get; set; }
    public float? TrustScore { get; set; }
    public int ProfileCompletionPercent { get; set; }
    public int? OverallRating { get; set; }
}