
using System.Text.Json;
using Footbally.Application.AI.DTOs.Input;
using Footbally.Application.AI.Prompts;
using Footbally.Application.Interfaces;

namespace Footbally.Application.AI.Agents;

public class RatingCardAgent : BaseAiAgent
{
    public RatingCardAgent(IAIProvider aiProvider) : base(aiProvider) { }

    public override string AgentType => "RatingCard";

    protected override string GetSystemPrompt() => RatingCardPromptBuilder.SystemPrompt;

    protected override string GetUserPrompt(string inputJson)
    {
        var input = JsonSerializer.Deserialize<RatingCardInput>(inputJson,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            ?? new RatingCardInput();
        return RatingCardPromptBuilder.BuildUserPrompt(input);
    }
}