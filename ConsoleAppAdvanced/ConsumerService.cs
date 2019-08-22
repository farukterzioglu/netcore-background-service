using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ConsoleApp
{
    public class ConsumerService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly ConcurrentQueue<string> _queue;
        
        public ConsumerService(ILogger<ApplicationService> logger, ConcurrentQueue<string> queue)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _queue = queue ?? throw new ArgumentNullException(nameof(queue));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);

                if(_queue.TryDequeue(out string message)) {
                    _logger.LogInformation($"Got a new message: {message}. (Queue size: {_queue.Count})");
                }
            }
        }
    }
}