using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;

namespace BackgroundServices.Jobs
{
    public class HelloWorldService : IJob
    {
        public const string ServiceName = "HelloWorldService";
        private ILogger<HelloWorldService> _logger;

        public async Task Execute(IJobExecutionContext context)
        {
            _logger = context.MergedJobDataMap["logger"] as ILogger<HelloWorldService>;
            await Task.Run(() => RunMainProcess());
        }

        private void RunMainProcess()
        {
            _logger.LogDebug($"{DateTime.Now:yyyy-MM-dd hh:mm:ss}: Hello World!");
        }
    }
}
