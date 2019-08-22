using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ConsoleApp
{
    public class AnotherService : BackgroundService
    {
        private readonly ILogger _logger;
        private bool _isStopping;

        public AnotherService(ILogger<ApplicationService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Another service started...");
            
            try
            {
                while(!stoppingToken.IsCancellationRequested && !_isStopping)
                {
                    _logger.LogDebug($"Another service processing...{stoppingToken.IsCancellationRequested}");
                    await Task.Delay(1000, stoppingToken);
                    // throw new Exception();
                }
            }
            catch (TaskCanceledException) {}
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
                await StopAsync(stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("AnotherService stopping...");
            _isStopping = true;

            // Gracefully shutdown logic -> 
            await Task.Delay(2000);

            _logger.LogInformation("AnotherService stopped.");
            await base.StopAsync(stoppingToken);
        }
    }
}