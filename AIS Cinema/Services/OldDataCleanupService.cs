using Microsoft.EntityFrameworkCore;

namespace AIS_Cinema.Services
{
    public class OldDataCleanupService : BackgroundService
    {

        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<OldDataCleanupService> _logger;
        private readonly ImageWorker _imageWorker;
        private const int MonthsThreshold = 3;

        public OldDataCleanupService(IServiceProvider serviceProvider, ILogger<OldDataCleanupService> logger, ImageWorker imageWorker)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
            _imageWorker = imageWorker;
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

                try
                {
                    await CleanupOldMoviesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while cleaning up old movies.");
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

        private async Task CleanupOldMoviesAsync()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AISCinemaDbContext>();

                var currentDate = DateTime.UtcNow;
                var oldMovies = await context.Movies
                    .Include(m => m.Sessions)
                    .Where(m => m.Sessions.Count == 0 && EF.Functions.DateDiffMonth(m.ReleaseDate, currentDate) >= MonthsThreshold)
                    .ToListAsync();

                foreach (var movie in oldMovies)
                {
                    _imageWorker.DeleteImage(movie.PosterPath);
                }

                context.Movies.RemoveRange(oldMovies);
                await context.SaveChangesAsync();

                _logger.LogDebug("Old movies have been deleted");
            }
        }
    }
}
