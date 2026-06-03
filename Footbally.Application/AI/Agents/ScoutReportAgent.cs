
using System.Text.Json;
using Footbally.Application.AI.DTOs.Input;
using Footbally.Application.AI.Prompts;
using Footbally.Application.Interfaces;

namespace Footbally.Application.AI.Agents;

public class ScoutReportAgent : BaseAiAgent
{
    public ScoutReportAgent(IAIProvider aiProvider) : base(aiProvider) { }

    public override string AgentType => "ScoutReport";

    protected override string GetSystemPrompt() => ScoutReportPromptBuilder.SystemPrompt;

    protected override string GetUserPrompt(string inputJson)
    {
        var input = JsonSerializer.Deserialize<ScoutReportInput>(inputJson,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            ?? new ScoutReportInput();
        return ScoutReportPromptBuilder.BuildUserPrompt(input);
    }
}