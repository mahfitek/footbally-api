
using System.Text.Json;
using Footbally.Application.AI.DTOs.Input;
using Footbally.Application.AI.Prompts;
using Footbally.Application.Interfaces;

namespace Footbally.Application.AI.Agents;

public class FootballCvAgent : BaseAiAgent
{
    public FootballCvAgent(IAIProvider aiProvider) : base(aiProvider) { }
    public override string AgentType => "FootballCv";
    protected override string GetSystemPrompt() => FootballCvPromptBuilder.SystemPrompt;
    protected override string GetUserPrompt(string inputJson)
    {
        var input = JsonSerializer.Deserialize<FootballCvInput>(inputJson,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new FootballCvInput();
        return FootballCvPromptBuilder.BuildUserPrompt(input);
    }
}