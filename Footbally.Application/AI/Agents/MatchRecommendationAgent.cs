
using System.Text.Json;
using Footbally.Application.AI.DTOs.Input;
using Footbally.Application.AI.Prompts;
using Footbally.Application.Interfaces;

namespace Footbally.Application.AI.Agents;

public class MatchRecommendationAgent : BaseAiAgent
{
    public MatchRecommendationAgent(IAIProvider aiProvider) : base(aiProvider) { }
    public override string AgentType => "MatchRecommendation";
    protected override string GetSystemPrompt() => MatchRecommendationPromptBuilder.SystemPrompt;
    protected override string GetUserPrompt(string inputJson)
    {
        var input = JsonSerializer.Deserialize<MatchRecommendationInput>(inputJson,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new MatchRecommendationInput();
        return MatchRecommendationPromptBuilder.BuildUserPrompt(input);
    }
}