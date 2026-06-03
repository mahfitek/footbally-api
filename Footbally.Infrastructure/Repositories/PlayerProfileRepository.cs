using Footbally.Application.DTOs.Player;
using Footbally.Application.Interfaces;
using Footbally.Domain.Entities;
using Footbally.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Footbally.Infrastructure.Repositories;

public class PlayerProfileRepository : IPlayerProfileService
{
    private readonly FootballyDbContext _context;

    public PlayerProfileRepository(FootballyDbContext context)
    {
        _context = context;
    }

    public async Task<List<PlayerProfileDto>> GetAllAsync()
    {
        return await _context.PlayerProfiles
            .Include(p => p.User)
            .Select(p => MapToDto(p))
            .ToListAsync();
    }

    public async Task<PlayerProfileDto?> GetByUserIdAsync(int userId)
    {
        var profile = await _context.PlayerProfiles
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.UserId == userId);

        if (profile == null) return null;
        return MapToDto(profile);
    }

    public async Task<PlayerProfileDto> UpsertAsync(int userId, UpsertPlayerProfileDto dto)
    {
        var profile = await _context.PlayerProfiles
            .FirstOrDefaultAsync(p => p.UserId == userId);

        if (profile == null)
        {
            profile = new PlayerProfile
            {
                UserId = userId,
                Position = dto.Position,
                Foot = dto.Foot,
                Height = dto.Height,
                Weight = dto.Weight,
                Age = dto.Age,
                City = dto.City,
                Bio = dto.Bio,
                AvatarUrl = dto.AvatarUrl,
                IsAvailable = dto.IsAvailable,
                UpdatedAt = DateTime.UtcNow
            };
            _context.PlayerProfiles.Add(profile);
        }
        else
        {
            profile.Position = dto.Position;
            profile.Foot = dto.Foot;
            profile.Height = dto.Height;
            profile.Weight = dto.Weight;
            profile.Age = dto.Age;
            profile.City = dto.City;
            profile.Bio = dto.Bio;
            profile.AvatarUrl = dto.AvatarUrl;
            profile.IsAvailable = dto.IsAvailable;
            profile.UpdatedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();

        var user = await _context.Users.FindAsync(userId);
        profile.User = user!;

        return MapToDto(profile);
    }

    private static PlayerProfileDto MapToDto(PlayerProfile p) => new()
    {
        Id = p.Id,
        UserId = p.UserId,
        FullName = p.User?.FullName ?? string.Empty,
        Position = p.Position,
        Foot = p.Foot,
        Height = p.Height,
        Weight = p.Weight,
        Age = p.Age,
        City = p.City,
        Bio = p.Bio,
        AvatarUrl = p.AvatarUrl,
        IsAvailable = p.IsAvailable
    };
}