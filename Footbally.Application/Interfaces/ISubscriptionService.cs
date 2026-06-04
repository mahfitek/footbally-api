using Footbally.Application.DTOs.Subscription;

namespace Footbally.Application.Interfaces;

public interface ISubscriptionService
{
    Task<List<SubscriptionResponseDto>> GetAllAsync();
    Task<SubscriptionResponseDto?> ExtendAsync(int id);
    Task<SubscriptionResponseDto?> CancelAsync(int id);
    Task<SubscriptionResponseDto> GrantAsync(GrantSubscriptionDto dto);
}
