using BobrVerse.Dal.Context;
using Microsoft.EntityFrameworkCore;

namespace BobrVerse.Api.HostedServices
{
    public class DailyLogsService(IServiceScopeFactory scopeFactory) : IHostedService, IDisposable
    {
        private Timer? _timer;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var now = DateTime.UtcNow;
            var nextRunTime = now.AddDays(1).AddHours(0);
            var initialDelay = nextRunTime - now;

            _timer = new Timer(DoWork, null, initialDelay, TimeSpan.FromDays(1));

            return Task.CompletedTask;
        }

        private async void DoWork(object? state)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                Console.WriteLine(1);
                var dbContext = scope.ServiceProvider.GetRequiredService<BobrVerseContext>();

                var bobrProfiles = await dbContext.BobrProfiles.Include(bp => bp.Level).ToListAsync();

                foreach (var bobrProfile in bobrProfiles)
                {
                    bobrProfile.Logs += bobrProfile.Level.LogsToAdd;
                }

                await dbContext.SaveChangesAsync();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}