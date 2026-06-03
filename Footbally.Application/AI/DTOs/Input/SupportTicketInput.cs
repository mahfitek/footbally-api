
namespace Footbally.Application.AI.DTOs.Input;

public class SupportTicketInput
{
    public Guid TicketId { get; set; }
    public string? Subject { get; set; }
    public string? Description { get; set; }
    public Guid UserId { get; set; }
    public string? UserRole { get; set; }
}