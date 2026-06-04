using Footbally.Application.DTOs.Subscription;

namespace Footbally.Application.DTOs.User;

public class AdminUserDetailDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? City { get; set; }
    public string? Position { get; set; }
    public int? Age { get; set; }
    public string? Bio { get; set; }
    public SubscriptionResponseDto? Subscription { get; set; }
}
