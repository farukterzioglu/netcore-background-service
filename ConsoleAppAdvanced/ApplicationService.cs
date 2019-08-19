using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ConsoleApp
{
    public class ApplicationService : BackgroundService
    {
        private readonly IRepository _repository;
        private readonly ApplicationSettings _settings;
        private readonly ILogger _logger;
        
        public ApplicationService(
            IRepository repository, 
            IOptions<ApplicationSettings> settings,
            ILogger<ApplicationService> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _settings = (settings ?? throw new ArgumentNullException(nameof(settings))).Value;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("ApplicationService started...");

            while(!stoppingToken.IsCancellationRequested)
            {
                _logger.LogDebug("Hello world from {0}", _settings.Environment);
                await Task.Delay(10000, stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("ApplicationService stopped.");

            await base.StopAsync(cancellationToken);
        }
    }
}