
using System.Text.Json;
using Footbally.Application.AI.DTOs.Input;
using Footbally.Application.AI.Prompts;
using Footbally.Application.Interfaces;

namespace Footbally.Application.AI.Agents;

public class PlayerComparisonAgent : BaseAiAgent
{
    public PlayerComparisonAgent(IAIProvider aiProvider) : base(aiProvider) { }
    public override string AgentType => "PlayerComparison";
    protected override string GetSystemPrompt() => PlayerComparisonPromptBuilder.SystemPrompt;
    protected override string GetUserPrompt(string inputJson)
    {
        var input = JsonSerializer.Deserialize<PlayerComparisonInput>(inputJson,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new PlayerComparisonInput();
        return PlayerComparisonPromptBuilder.BuildUserPrompt(input);
    }
}