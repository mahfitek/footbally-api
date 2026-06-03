
using System.Text.Json;
using Footbally.Application.AI.DTOs.Input;
using Footbally.Application.AI.Prompts;
using Footbally.Application.Interfaces;

namespace Footbally.Application.AI.Agents;

public class ContentQualityAgent : BaseAiAgent
{
    public ContentQualityAgent(IAIProvider aiProvider) : base(aiProvider) { }
    public override string AgentType => "ContentQuality";
    protected override string GetSystemPrompt() => ContentQualityPromptBuilder.SystemPrompt;
    protected override string GetUserPrompt(string inputJson)
    {
        var input = JsonSerializer.Deserialize<ContentQualityInput>(inputJson,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new ContentQualityInput();
        return ContentQualityPromptBuilder.BuildUserPrompt(input);
    }
}