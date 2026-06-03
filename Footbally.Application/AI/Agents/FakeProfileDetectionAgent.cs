
using System.Text.Json;
using Footbally.Application.AI.DTOs.Input;
using Footbally.Application.AI.Prompts;
using Footbally.Application.Interfaces;

namespace Footbally.Application.AI.Agents;

public class FakeProfileDetectionAgent : BaseAiAgent
{
    public FakeProfileDetectionAgent(IAIProvider aiProvider) : base(aiProvider) { }
    public override string AgentType => "FakeProfileDetection";
    protected override string GetSystemPrompt() => FakeProfileDetectionPromptBuilder.SystemPrompt;
    protected override string GetUserPrompt(string inputJson)
    {
        var input = JsonSerializer.Deserialize<FakeProfileDetectionInput>(inputJson,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new FakeProfileDetectionInput();
        return FakeProfileDetectionPromptBuilder.BuildUserPrompt(input);
    }
}