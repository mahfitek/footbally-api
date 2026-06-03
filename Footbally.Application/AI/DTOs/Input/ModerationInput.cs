
namespace Footbally.Application.AI.DTOs.Input;

public class ModerationInput
{
    public Guid EntityId { get; set; }
    public string EntityType { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public Guid? AuthorId { get; set; }
}