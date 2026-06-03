
using System.Text.Json;
using Footbally.Application.AI.DTOs.Input;
using Footbally.Application.AI.Prompts;
using Footbally.Application.Interfaces;

namespace Footbally.Application.AI.Agents;

public class ScoutMessageAgent : BaseAiAgent
{
    public ScoutMessageAgent(IAIProvider aiProvider) : base(aiProvider) { }
    public override string AgentType => "ScoutMessage";
    protected override string GetSystemPrompt() => ScoutMessagePromptBuilder.SystemPrompt;
    protected override string GetUserPrompt(string inputJson)
    {
        var input = JsonSerializer.Deserialize<ScoutMessageInput>(inputJson,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new ScoutMessageInput();
        return ScoutMessagePromptBuilder.BuildUserPrompt(input);
    }
}