using Footbally.Application.DTOs.Subscription;
using Footbally.Application.Interfaces;
using Footbally.Domain.Entities;
using Footbally.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Footbally.Infrastructure.Repositories;

public class SubscriptionRepository : ISubscriptionService
{
    private readonly FootballyDbContext _context;

    public SubscriptionRepository(FootballyDbContext context)
    {
        _context = context;
    }

    public async Task<List<SubscriptionResponseDto>> GetAllAsync()
    {
        return await _context.Subscriptions
            .Include(s => s.User)
            .OrderByDescending(s => s.StartDate)
            .Select(s => new SubscriptionResponseDto
            {
                Id = s.Id,
                UserId = s.UserId,
                FullName = s.User.FullName,
                Email = s.User.Email,
                PlanType = s.PlanType,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                Status = s.Status
            })
            .ToListAsync();
    }

    public async Task<SubscriptionResponseDto?> ExtendAsync(int id)
    {
        var sub = await _context.Subscriptions
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (sub == null) return null;

        sub.EndDate = sub.EndDate < DateTime.UtcNow
            ? DateTime.UtcNow.AddDays(30)
            : sub.EndDate.AddDays(30);
        sub.Status = "Active";
        await _context.SaveChangesAsync();

        return MapToDto(sub);
    }

    public async Task<SubscriptionResponseDto?> CancelAsync(int id)
    {
        var sub = await _context.Subscriptions
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (sub == null) return null;

        sub.Status = "Cancelled";
        await _context.SaveChangesAsync();

        return MapToDto(sub);
    }

    public async Task<SubscriptionResponseDto> GrantAsync(GrantSubscriptionDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (user == null) throw new InvalidOperationException("Kullanıcı bulunamadı.");

        var existing = await _context.Subscriptions
            .FirstOrDefaultAsync(s => s.UserId == user.Id && s.Status == "Active");

        if (existing != null)
        {
            existing.PlanType = dto.PlanType;
            existing.EndDate = DateTime.UtcNow.AddDays(30);
            await _context.SaveChangesAsync();
            existing.User = user;
            return MapToDto(existing);
        }

        var sub = new Subscription
        {
            UserId = user.Id,
            PlanType = dto.PlanType,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddDays(30),
            Status = "Active"
        };

        _context.Subscriptions.Add(sub);
        await _context.SaveChangesAsync();
        sub.User = user;

        return MapToDto(sub);
    }

    private static SubscriptionResponseDto MapToDto(Subscription s) => new()
    {
        Id = s.Id,
        UserId = s.UserId,
        FullName = s.User?.FullName,
        Email = s.User?.Email,
        PlanType = s.PlanType,
        StartDate = s.StartDate,
        EndDate = s.EndDate,
        Status = s.Status
    };
}
