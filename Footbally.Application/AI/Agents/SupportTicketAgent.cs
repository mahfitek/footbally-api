
using System.Text.Json;
using Footbally.Application.AI.DTOs.Input;
using Footbally.Application.AI.Prompts;
using Footbally.Application.Interfaces;

namespace Footbally.Application.AI.Agents;

public class SupportTicketAgent : BaseAiAgent
{
    public SupportTicketAgent(IAIProvider aiProvider) : base(aiProvider) { }
    public override string AgentType => "SupportTicket";
    protected override string GetSystemPrompt() => SupportTicketPromptBuilder.SystemPrompt;
    protected override string GetUserPrompt(string inputJson)
    {
        var input = JsonSerializer.Deserialize<SupportTicketInput>(inputJson,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new SupportTicketInput();
        return SupportTicketPromptBuilder.BuildUserPrompt(input);
    }
}