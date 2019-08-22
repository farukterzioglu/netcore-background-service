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
        private readonly ExternalSettings _externalSettings;
        private readonly ILogger _logger;
        private readonly IApplicationLifetime _appLifetime;

        public ApplicationService(
            IRepository repository, 
            IOptions<ApplicationSettings> settings,
            IOptions<ExternalSettings> externalSettings,
            IOptions<ServerSettings> serverSettings,
            ILogger<ApplicationService> logger, 
            IApplicationLifetime appLifetime)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _settings = (settings ?? throw new ArgumentNullException(nameof(settings))).Value;
            _externalSettings = (externalSettings ?? throw new ArgumentNullException(nameof(externalSettings))).Value;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _appLifetime = appLifetime ?? throw new ArgumentNullException(nameof(appLifetime));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("ApplicationService started...");

            try
            {
                while(!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogDebug("Hello world from {0}. Value: {1}", _settings.Environment, _externalSettings.SomeValue);
                    await Task.Delay(4000, stoppingToken);
                    // throw new Exception();
                }
            }
            catch (TaskCanceledException ex) { 
                _logger.LogWarning(ex.ToString());
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
                _appLifetime.StopApplication();
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("ApplicationService stopped.");
            await base.StopAsync(cancellationToken);
        }
    }
}