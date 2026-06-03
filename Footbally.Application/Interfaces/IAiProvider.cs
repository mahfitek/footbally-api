namespace Footbally.Application.Interfaces;

public interface IAIProvider
{
    Task<AIProviderResult> CompleteAsync(string systemPrompt, string userPrompt, CancellationToken cancellationToken = default);
}

public class AIProviderResult
{
    public string Content { get; set; } = string.Empty;
    public int TokensUsed { get; set; }
    public string ModelUsed { get; set; } = string.Empty;
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }
}