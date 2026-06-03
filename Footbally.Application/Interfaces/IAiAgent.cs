namespace Footbally.Application.Interfaces;

public interface IAiAgent
{
    string AgentType { get; }
    Task<AiAgentResult> RunAsync(string inputJson, CancellationToken cancellationToken = default);
}

public class AiAgentResult
{
    public string OutputJson { get; set; } = string.Empty;
    public float ConfidenceScore { get; set; }
    public bool AdminReviewRequired { get; set; }
    public int TokensUsed { get; set; }
    public string ModelUsed { get; set; } = string.Empty;
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
}