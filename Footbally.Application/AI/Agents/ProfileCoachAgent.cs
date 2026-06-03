
using System.Text.Json;
using Footbally.Application.AI.DTOs.Input;
using Footbally.Application.AI.Prompts;
using Footbally.Application.Interfaces;

namespace Footbally.Application.AI.Agents;

public class ProfileCoachAgent : BaseAiAgent
{
    public ProfileCoachAgent(IAIProvider aiProvider) : base(aiProvider) { }

    public override string AgentType => "ProfileCoach";

    protected override string GetSystemPrompt() => ProfileCoachPromptBuilder.SystemPrompt;

    protected override string GetUserPrompt(string inputJson)
    {
        var input = JsonSerializer.Deserialize<ProfileCoachInput>(inputJson,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
            ?? new ProfileCoachInput();
        return ProfileCoachPromptBuilder.BuildUserPrompt(input);
    }
}