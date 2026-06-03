
using System.Text.Json;
using Footbally.Application.Interfaces;

namespace Footbally.Application.AI.Agents;

public abstract class BaseAiAgent : IAiAgent
{
    protected readonly IAIProvider _aiProvider;

    protected BaseAiAgent(IAIProvider aiProvider)
    {
        _aiProvider = aiProvider;
    }

    public abstract string AgentType { get; }
    protected abstract string GetSystemPrompt();
    protected abstract string GetUserPrompt(string inputJson);

    public async Task<AiAgentResult> RunAsync(string inputJson, CancellationToken cancellationToken = default)
    {
        var sanitized = SanitizeInput(inputJson);
        var systemPrompt = GetSystemPrompt();
        var userPrompt = GetUserPrompt(sanitized);

        var result = await _aiProvider.CompleteAsync(systemPrompt, userPrompt, cancellationToken);

        if (!result.Success)
        {
            return new AiAgentResult
            {
                Success = false,
                ErrorMessage = result.ErrorMessage
            };
        }

        if (!IsValidJson(result.Content))
        {
            return new AiAgentResult
            {
                Success = false,
                ErrorMessage = "AI response is not valid JSON."
            };
        }

        var confidenceScore = ExtractConfidenceScore(result.Content);
        var adminReviewRequired = ExtractAdminReviewRequired(result.Content);

        return new AiAgentResult
        {
            OutputJson = result.Content,
            ConfidenceScore = confidenceScore,
            AdminReviewRequired = adminReviewRequired,
            TokensUsed = result.TokensUsed,
            ModelUsed = result.ModelUsed,
            Success = true
        };
    }

    private static string SanitizeInput(string input)
    {
        if (string.IsNullOrWhiteSpace(input)) return input;
        return input
            .Replace("```", "")
            .Replace("<|system|>", "")
            .Replace("<|user|>", "")
            .Replace("<|assistant|>", "");
    }

    private static bool IsValidJson(string content)
    {
        try
        {
            JsonDocument.Parse(content);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private static float ExtractConfidenceScore(string json)
    {
        try
        {
            using var doc = JsonDocument.Parse(json);
            if (doc.RootElement.TryGetProperty("confidenceScore", out var prop))
                return prop.GetSingle();
        }
        catch { }
        return 0.5f;
    }

    private static bool ExtractAdminReviewRequired(string json)
    {
        try
        {
            using var doc = JsonDocument.Parse(json);
            if (doc.RootElement.TryGetProperty("adminReviewRequired", out var prop))
                return prop.GetBoolean();
        }
        catch { }
        return false;
    }
}