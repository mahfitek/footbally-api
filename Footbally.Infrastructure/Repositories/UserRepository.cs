using Footbally.Application.DTOs.Subscription;
using Footbally.Application.DTOs.User;
using Footbally.Application.Interfaces;
using Footbally.Domain.Entities;
using Footbally.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Footbally.Infrastructure.Repositories;

public class UserRepository : IUserService
{
    private readonly FootballyDbContext _context;

    public UserRepository(FootballyDbContext context)
    {
        _context = context;
    }

    public async Task<UserResponseDto?> GetByIdAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return null;
        return MapToDto(user);
    }

    public async Task<UserResponseDto?> GetByEmailAsync(string email)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) return null;
        return MapToDto(user);
    }

    public async Task<UserResponseDto> CreateAsync(RegisterRequestDto dto)
    {
        var user = new User
        {
            FullName = dto.FullName,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = dto.Role,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return MapToDto(user);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<UserResponseDto>> GetAllAsync()
    {
        return await _context.Users
            .OrderByDescending(u => u.CreatedAt)
            .Select(u => MapToDto(u))
            .ToListAsync();
    }

    public async Task<bool> SetActiveAsync(int id, bool isActive)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;
        user.IsActive = isActive;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<AdminUserDetailDto?> GetDetailAsync(int id)
    {
        var user = await _context.Users
            .Include(u => u.PlayerProfile)
            .Include(u => u.Subscriptions)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null) return null;

        var latestSub = user.Subscriptions
            .OrderByDescending(s => s.EndDate)
            .FirstOrDefault();

        return new AdminUserDetailDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role,
            IsActive = user.IsActive,
            CreatedAt = user.CreatedAt,
            City = user.PlayerProfile?.City,
            Position = user.PlayerProfile?.Position,
            Age = user.PlayerProfile?.Age,
            Bio = user.PlayerProfile?.Bio,
            Subscription = latestSub == null ? null : new SubscriptionResponseDto
            {
                Id = latestSub.Id,
                UserId = latestSub.UserId,
                FullName = user.FullName,
                Email = user.Email,
                PlanType = latestSub.PlanType,
                StartDate = latestSub.StartDate,
                EndDate = latestSub.EndDate,
                Status = latestSub.Status
            }
        };
    }

    private static UserResponseDto MapToDto(User user) => new()
    {
        Id = user.Id,
        FullName = user.FullName,
        Email = user.Email,
        Role = user.Role,
        CreatedAt = user.CreatedAt,
        IsActive = user.IsActive
    };
}