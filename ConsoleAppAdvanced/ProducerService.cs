using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ConsoleApp
{
    public class ProducerService : BackgroundService
    {
        private readonly ConcurrentQueue<string> _queue;

        public ProducerService(ConcurrentQueue<string> queue)
        {
            _queue = queue ?? throw new ArgumentNullException(nameof(queue));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(500, stoppingToken);
                _queue.Enqueue($"Message : {DateTime.Now.ToString()}");
            }
        }
    }
}