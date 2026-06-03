
using System.Text.Json;
using Footbally.Application.AI.DTOs.Input;
using Footbally.Application.AI.Prompts;
using Footbally.Application.Interfaces;

namespace Footbally.Application.AI.Agents;

public class CareerCoachAgent : BaseAiAgent
{
    public CareerCoachAgent(IAIProvider aiProvider) : base(aiProvider) { }
    public override string AgentType => "CareerCoach";
    protected override string GetSystemPrompt() => CareerCoachPromptBuilder.SystemPrompt;
    protected override string GetUserPrompt(string inputJson)
    {
        var input = JsonSerializer.Deserialize<CareerCoachInput>(inputJson,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new CareerCoachInput();
        return CareerCoachPromptBuilder.BuildUserPrompt(input);
    }
}