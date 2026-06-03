using Footbally.Application.DTOs.Match;

namespace Footbally.Application.Interfaces;

public interface IMatchService
{
    Task<List<MatchResponseDto>> GetAllAsync();
    Task<MatchResponseDto?> GetByIdAsync(int id);
    Task<MatchResponseDto> CreateAsync(CreateMatchDto dto);
    Task<bool> DeleteAsync(int id);
    Task<MatchApplicationDto> ApplyAsync(int matchId, int userId);
    Task<List<MatchApplicationDto>> GetApplicationsAsync(int matchId);
    Task<MatchApplicationDto?> UpdateApplicationStatusAsync(int matchId, int applicationId, string status);
}