using Footbally.Domain.Entities;

namespace Footbally.Application.Interfaces;

public interface IAiJobRepository
{
    Task<AiJob> CreateAsync(AiJob job, CancellationToken cancellationToken = default);
    Task<AiJob?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<AiJob>> GetPendingJobsAsync(int batchSize = 10, CancellationToken cancellationToken = default);
    Task<List<AiJob>> GetByEntityIdAsync(Guid entityId, string entityType, CancellationToken cancellationToken = default);
    Task<AiJob?> GetLatestByEntityAsync(Guid entityId, string jobType, CancellationToken cancellationToken = default);
    Task UpdateAsync(AiJob job, CancellationToken cancellationToken = default);
    Task<List<AiJob>> GetAllForAdminAsync(int page, int pageSize, string? statusFilter, string? jobTypeFilter, CancellationToken cancellationToken = default);
    Task<int> CountForAdminAsync(string? statusFilter, string? jobTypeFilter, CancellationToken cancellationToken = default);
}