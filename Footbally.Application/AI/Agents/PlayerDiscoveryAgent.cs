
using System.Text.Json;
using Footbally.Application.AI.DTOs.Input;
using Footbally.Application.AI.Prompts;
using Footbally.Application.Interfaces;

namespace Footbally.Application.AI.Agents;

public class PlayerDiscoveryAgent : BaseAiAgent
{
    public PlayerDiscoveryAgent(IAIProvider aiProvider) : base(aiProvider) { }
    public override string AgentType => "PlayerDiscovery";
    protected override string GetSystemPrompt() => PlayerDiscoveryPromptBuilder.SystemPrompt;
    protected override string GetUserPrompt(string inputJson)
    {
        var input = JsonSerializer.Deserialize<PlayerDiscoveryInput>(inputJson,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new PlayerDiscoveryInput();
        return PlayerDiscoveryPromptBuilder.BuildUserPrompt(input);
    }
}