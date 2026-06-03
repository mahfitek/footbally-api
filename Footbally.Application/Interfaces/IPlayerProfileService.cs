using Footbally.Application.DTOs.Player;

namespace Footbally.Application.Interfaces;

public interface IPlayerProfileService
{
    Task<PlayerProfileDto?> GetByUserIdAsync(int userId);
    Task<PlayerProfileDto> UpsertAsync(int userId, UpsertPlayerProfileDto dto);
    Task<List<PlayerProfileDto>> GetAllAsync();
}