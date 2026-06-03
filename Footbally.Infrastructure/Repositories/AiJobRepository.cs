
using Footbally.Application.Interfaces;
using Footbally.Domain.Entities;
using Footbally.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Footbally.Infrastructure.Repositories;

public class AiJobRepository : IAiJobRepository
{
    private readonly FootballyDbContext _context;

    public AiJobRepository(FootballyDbContext context)
    {
        _context = context;
    }

    public async Task<AiJob> CreateAsync(AiJob job, CancellationToken cancellationToken = default)
    {
        _context.AiJobs.Add(job);
        await _context.SaveChangesAsync(cancellationToken);
        return job;
    }

    public async Task<AiJob?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.AiJobs.FindAsync([id], cancellationToken);
    }

    public async Task<List<AiJob>> GetPendingJobsAsync(int batchSize = 10, CancellationToken cancellationToken = default)
    {
        return await _context.AiJobs
            .Where(j => j.Status == "Pending")
            .OrderBy(j => j.CreatedAt)
            .Take(batchSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<List<AiJob>> GetByEntityIdAsync(Guid entityId, string entityType, CancellationToken cancellationToken = default)
    {
        return await _context.AiJobs
            .Where(j => j.TargetEntityId == entityId && j.TargetEntityType == entityType)
            .OrderByDescending(j => j.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<AiJob?> GetLatestByEntityAsync(Guid entityId, string jobType, CancellationToken cancellationToken = default)
    {
        return await _context.AiJobs
            .Where(j => j.TargetEntityId == entityId && j.JobType == jobType && j.Status == "Completed")
            .OrderByDescending(j => j.CreatedAt)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task UpdateAsync(AiJob job, CancellationToken cancellationToken = default)
    {
        _context.AiJobs.Update(job);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<AiJob>> GetAllForAdminAsync(int page, int pageSize, string? statusFilter, string? jobTypeFilter, CancellationToken cancellationToken = default)
    {
        var query = _context.AiJobs.AsQueryable();

        if (!string.IsNullOrEmpty(statusFilter))
            query = query.Where(j => j.Status == statusFilter);

        if (!string.IsNullOrEmpty(jobTypeFilter))
            query = query.Where(j => j.JobType == jobTypeFilter);

        return await query
            .OrderByDescending(j => j.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> CountForAdminAsync(string? statusFilter, string? jobTypeFilter, CancellationToken cancellationToken = default)
    {
        var query = _context.AiJobs.AsQueryable();

        if (!string.IsNullOrEmpty(statusFilter))
            query = query.Where(j => j.Status == statusFilter);

        if (!string.IsNullOrEmpty(jobTypeFilter))
            query = query.Where(j => j.JobType == jobTypeFilter);

        return await query.CountAsync(cancellationToken);
    }
}