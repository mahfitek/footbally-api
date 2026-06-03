using Footbally.Application.DTOs.Team;
using Footbally.Application.Interfaces;
using Footbally.Domain.Entities;
using Footbally.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Footbally.Infrastructure.Repositories;

public class TeamRepository : ITeamService
{
    private readonly FootballyDbContext _context;

    public TeamRepository(FootballyDbContext context)
    {
        _context = context;
    }

    public async Task<List<TeamResponseDto>> GetAllAsync()
    {
        return await _context.Teams
            .Select(t => MapToDto(t))
            .ToListAsync();
    }

    public async Task<TeamResponseDto?> GetByIdAsync(int id)
    {
        var team = await _context.Teams.FindAsync(id);
        if (team == null) return null;
        return MapToDto(team);
    }

    public async Task<TeamResponseDto> CreateAsync(CreateTeamDto dto)
    {
        var team = new Team
        {
            Name = dto.Name,
            City = dto.City,
            OwnerId = dto.OwnerId,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        _context.Teams.Add(team);
        await _context.SaveChangesAsync();
        return MapToDto(team);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var team = await _context.Teams.FindAsync(id);
        if (team == null) return false;
        _context.Teams.Remove(team);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<TeamProfileResponseDto> UpsertProfileAsync(int teamId, UpsertTeamProfileDto dto)
    {
        var team = await _context.Teams.FindAsync(teamId);
        if (team == null) throw new InvalidOperationException("Takım bulunamadı.");

        var profile = await _context.TeamProfiles
            .FirstOrDefaultAsync(p => p.TeamId == teamId);

        if (profile == null)
        {
            profile = new TeamProfile
            {
                TeamId = teamId,
                Description = dto.Description,
                LogoUrl = dto.LogoUrl,
                Level = dto.Level,
                FoundedYear = dto.FoundedYear,
                PreferredFormat = dto.PreferredFormat,
                MatchDays = dto.MatchDays,
                NeededPositions = dto.NeededPositions,
                IsLookingForPlayers = dto.IsLookingForPlayers,
                UpdatedAt = DateTime.UtcNow
            };
            _context.TeamProfiles.Add(profile);
        }
        else
        {
            profile.Description = dto.Description;
            profile.LogoUrl = dto.LogoUrl;
            profile.Level = dto.Level;
            profile.FoundedYear = dto.FoundedYear;
            profile.PreferredFormat = dto.PreferredFormat;
            profile.MatchDays = dto.MatchDays;
            profile.NeededPositions = dto.NeededPositions;
            profile.IsLookingForPlayers = dto.IsLookingForPlayers;
            profile.UpdatedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();

        return new TeamProfileResponseDto
        {
            Id = profile.Id,
            TeamId = teamId,
            TeamName = team.Name,
            City = team.City,
            Description = profile.Description,
            LogoUrl = profile.LogoUrl,
            Level = profile.Level,
            FoundedYear = profile.FoundedYear,
            PreferredFormat = profile.PreferredFormat,
            MatchDays = profile.MatchDays,
            NeededPositions = profile.NeededPositions,
            IsLookingForPlayers = profile.IsLookingForPlayers,
            UpdatedAt = profile.UpdatedAt
        };
    }

    public async Task<TeamProfileResponseDto?> GetProfileByTeamIdAsync(int teamId)
    {
        var profile = await _context.TeamProfiles
            .Include(p => p.Team)
            .FirstOrDefaultAsync(p => p.TeamId == teamId);

        if (profile == null) return null;

        return new TeamProfileResponseDto
        {
            Id = profile.Id,
            TeamId = teamId,
            TeamName = profile.Team.Name,
            City = profile.Team.City,
            Description = profile.Description,
            LogoUrl = profile.LogoUrl,
            Level = profile.Level,
            FoundedYear = profile.FoundedYear,
            PreferredFormat = profile.PreferredFormat,
            MatchDays = profile.MatchDays,
            NeededPositions = profile.NeededPositions,
            IsLookingForPlayers = profile.IsLookingForPlayers,
            UpdatedAt = profile.UpdatedAt
        };
    }

    private static TeamResponseDto MapToDto(Team team) => new()
    {
        Id = team.Id,
        Name = team.Name,
        City = team.City,
        OwnerId = team.OwnerId,
        CreatedAt = team.CreatedAt,
        IsActive = team.IsActive
    };
}