using Microsoft.EntityFrameworkCore;

namespace AIS_Cinema.Services
{
    public class SessionCleanupService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<SessionCleanupService> _logger;

        public SessionCleanupService(IServiceProvider serviceProvider, ILogger<SessionCleanupService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await CleanupOldSessionsAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while cleaning up old sessions.");
                }

                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }

        private async Task CleanupOldSessionsAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AISCinemaDbContext>();

                var now = DateTime.UtcNow;
                var oldSessions = await context.Sessions
                    .Where(s => s.DateTime < now)
                    .ToListAsync();

                context.Sessions.RemoveRange(oldSessions);
                await context.SaveChangesAsync();

                _logger.LogDebug("Old sessions have been deleted");
            }
        }
    }
}
