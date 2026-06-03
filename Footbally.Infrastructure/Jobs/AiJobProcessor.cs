
using Footbally.Application.AI;
using Footbally.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Footbally.Infrastructure.Jobs;

public class AiJobProcessor : BackgroundService
{
    private readonly IServiceProvider _services;
    private readonly ILogger<AiJobProcessor> _logger;

    public AiJobProcessor(IServiceProvider services, ILogger<AiJobProcessor> logger)
    {
        _services = services;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("AiJobProcessor başladı.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _services.CreateScope();
                var repo = scope.ServiceProvider.GetRequiredService<IAiJobRepository>();
                var orchestrator = scope.ServiceProvider.GetRequiredService<AiOrchestrator>();

                var pendingJobs = await repo.GetPendingJobsAsync(batchSize: 5, stoppingToken);

                foreach (var job in pendingJobs)
                {
                    _logger.LogInformation("AI Job işleniyor: {JobId} - {JobType}", job.Id, job.JobType);
                    await orchestrator.ProcessAsync(job, stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AiJobProcessor hata aldı.");
            }

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }

        _logger.LogInformation("AiJobProcessor durdu.");
    }
}