
using System.Text.Json;
using Footbally.Application.AI.DTOs.Input;
using Footbally.Application.AI.Prompts;
using Footbally.Application.Interfaces;

namespace Footbally.Application.AI.Agents;

public class VideoSummaryAgent : BaseAiAgent
{
    public VideoSummaryAgent(IAIProvider aiProvider) : base(aiProvider) { }
    public override string AgentType => "VideoSummary";
    protected override string GetSystemPrompt() => VideoSummaryPromptBuilder.SystemPrompt;
    protected override string GetUserPrompt(string inputJson)
    {
        var input = JsonSerializer.Deserialize<VideoSummaryInput>(inputJson,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new VideoSummaryInput();
        return VideoSummaryPromptBuilder.BuildUserPrompt(input);
    }
}