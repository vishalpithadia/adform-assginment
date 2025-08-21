using Assignment.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace Assignment.Job;
public class SyncLatestRatesServiceJob : IHostedService, IDisposable
{

    private readonly IServiceProvider _services;
    public SyncLatestRatesServiceJob(IServiceProvider services)
    {
        _services = services;
    }
    private Timer? _timer;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(60));
        return Task.CompletedTask;
    }

    private void DoWork(object? state)
    {
        Console.WriteLine($"Hosted service running at: {DateTime.Now}");
        using (var scope = _services.CreateScope())
        { 
            var syncService = scope.ServiceProvider.GetRequiredService<ISyncLatestRatesService>();
            syncService.SyncLatestRatesAsync().GetAwaiter().GetResult();            
        }

        // Add your periodic logic here
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