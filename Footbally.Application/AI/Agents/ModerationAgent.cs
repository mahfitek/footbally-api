using System.Text.Json;
using Footbally.Application.AI.DTOs.Input;
using Footbally.Application.AI.Prompts;
using Footbally.Application.Interfaces;

namespace Footbally.Application.AI.Agents;

public class ModerationAgent : BaseAiAgent
{
    public ModerationAgent(IAIProvider aiProvider) : base(aiProvider) { }

    public override string AgentType => "Moderation";

    protected override string GetSystemPrompt() => ModerationPromptBuilder.SystemPrompt;

    protected override string GetUserPrompt(string inputJson)
    {
        var input = JsonSerializer.Deserialize<ModerationInput>(inputJson,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            ?? new ModerationInput();
        return ModerationPromptBuilder.BuildUserPrompt(input);
    }
}