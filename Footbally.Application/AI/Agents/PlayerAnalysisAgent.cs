
using System.Text.Json;
using Footbally.Application.AI.DTOs.Input;
using Footbally.Application.AI.Prompts;
using Footbally.Application.Interfaces;

namespace Footbally.Application.AI.Agents;

public class PlayerAnalysisAgent : BaseAiAgent
{
    public PlayerAnalysisAgent(IAIProvider aiProvider) : base(aiProvider) { }
    public override string AgentType => "PlayerAnalysis";
    protected override string GetSystemPrompt() => PlayerAnalysisPromptBuilder.SystemPrompt;
    protected override string GetUserPrompt(string inputJson)
    {
        var input = JsonSerializer.Deserialize<PlayerAnalysisInput>(inputJson,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new PlayerAnalysisInput();
        return PlayerAnalysisPromptBuilder.BuildUserPrompt(input);
    }
}