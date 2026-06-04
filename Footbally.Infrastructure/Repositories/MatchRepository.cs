using Footbally.Application.DTOs.Match;
using Footbally.Application.Interfaces;
using Footbally.Domain.Entities;
using Footbally.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Footbally.Infrastructure.Repositories;

public class MatchRepository : IMatchService
{
    private readonly FootballyDbContext _context;

    public MatchRepository(FootballyDbContext context)
    {
        _context = context;
    }

    public async Task<List<MatchResponseDto>> GetAllAsync()
    {
        return await _context.Matches
            .Include(m => m.HomeTeam)
            .Select(m => MapToDto(m))
            .ToListAsync();
    }

    public async Task<MatchResponseDto?> GetByIdAsync(int id)
    {
        var match = await _context.Matches
            .Include(m => m.HomeTeam)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (match == null) return null;
        return MapToDto(match);
    }

    public async Task<MatchResponseDto> CreateAsync(CreateMatchDto dto)
    {
        var match = new Match
        {
            HomeTeamId = dto.HomeTeamId,
            Location = dto.Location,
            MatchDate = dto.MatchDate,
            PlayerQuota = dto.PlayerQuota,
            Description = dto.Description,
            Status = "Open",
            CreatedAt = DateTime.UtcNow
        };

        _context.Matches.Add(match);
        await _context.SaveChangesAsync();

        var created = await _context.Matches
            .Include(m => m.HomeTeam)
            .FirstAsync(m => m.Id == match.Id);

        return MapToDto(created);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var match = await _context.Matches.FindAsync(id);
        if (match == null) return false;
        _context.Matches.Remove(match);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<MatchApplicationDto> ApplyAsync(int matchId, int userId)
    {
        var existing = await _context.MatchApplications
            .FirstOrDefaultAsync(a => a.MatchId == matchId && a.UserId == userId);

        if (existing != null)
            throw new InvalidOperationException("Bu maça zaten başvurdunuz.");

        var application = new MatchApplication
        {
            MatchId = matchId,
            UserId = userId,
            Status = "Pending",
            AppliedAt = DateTime.UtcNow
        };

        _context.MatchApplications.Add(application);
        await _context.SaveChangesAsync();

        var user = await _context.Users.FindAsync(userId);

        return new MatchApplicationDto
        {
            Id = application.Id,
            MatchId = application.MatchId,
            UserId = application.UserId,
            UserFullName = user?.FullName ?? string.Empty,
            Status = application.Status,
            AppliedAt = application.AppliedAt
        };
    }

    public async Task<List<MatchApplicationDto>> GetApplicationsAsync(int matchId)
    {
        return await _context.MatchApplications
            .Include(a => a.User)
            .Where(a => a.MatchId == matchId)
            .Select(a => new MatchApplicationDto
            {
                Id = a.Id,
                MatchId = a.MatchId,
                UserId = a.UserId,
                UserFullName = a.User.FullName,
                Status = a.Status,
                AppliedAt = a.AppliedAt
            })
            .ToListAsync();
    }

    public async Task<MatchApplicationDto?> UpdateApplicationStatusAsync(int matchId, int applicationId, string status)
    {
        var application = await _context.MatchApplications
            .Include(a => a.User)
            .FirstOrDefaultAsync(a => a.Id == applicationId && a.MatchId == matchId);

        if (application == null) return null;

        application.Status = status;
        await _context.SaveChangesAsync();

        return new MatchApplicationDto
        {
            Id = application.Id,
            MatchId = application.MatchId,
            UserId = application.UserId,
            UserFullName = application.User.FullName,
            Status = application.Status,
            AppliedAt = application.AppliedAt
        };
    }

    public async Task<int> GetCountAsync()
    {
        return await _context.Matches.CountAsync();
    }

    private static MatchResponseDto MapToDto(Match match) => new()
    {
        Id = match.Id,
        HomeTeamId = match.HomeTeamId,
        HomeTeamName = match.HomeTeam?.Name ?? string.Empty,
        Location = match.Location,
        MatchDate = match.MatchDate,
        Status = match.Status,
        PlayerQuota = match.PlayerQuota,
        Description = match.Description,
        CreatedAt = match.CreatedAt
    };
}