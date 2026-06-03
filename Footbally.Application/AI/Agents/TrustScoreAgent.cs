
using System.Text.Json;
using Footbally.Application.AI.DTOs.Input;
using Footbally.Application.AI.Prompts;
using Footbally.Application.Interfaces;

namespace Footbally.Application.AI.Agents;

public class TrustScoreAgent : BaseAiAgent
{
    public TrustScoreAgent(IAIProvider aiProvider) : base(aiProvider) { }

    public override string AgentType => "TrustScore";

    protected override string GetSystemPrompt() => TrustScorePromptBuilder.SystemPrompt;

    protected override string GetUserPrompt(string inputJson)
    {
        var input = JsonSerializer.Deserialize<TrustScoreInput>(inputJson,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            ?? new TrustScoreInput();
        return TrustScorePromptBuilder.BuildUserPrompt(input);
    }
}