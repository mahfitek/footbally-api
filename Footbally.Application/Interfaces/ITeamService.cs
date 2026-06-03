using Footbally.Application.DTOs.Team;

namespace Footbally.Application.Interfaces;

public interface ITeamService
{
    Task<List<TeamResponseDto>> GetAllAsync();
    Task<TeamResponseDto?> GetByIdAsync(int id);
    Task<TeamResponseDto> CreateAsync(CreateTeamDto dto);
    Task<bool> DeleteAsync(int id);
    Task<TeamProfileResponseDto> UpsertProfileAsync(int teamId, UpsertTeamProfileDto dto);
    Task<TeamProfileResponseDto?> GetProfileByTeamIdAsync(int teamId);
}