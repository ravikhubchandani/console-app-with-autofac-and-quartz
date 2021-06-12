using Autofac;
using Microsoft.Extensions.Configuration;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackgroundServices
{
    public class Program
    {
        private static ApplicationSettings _settings;
        private static IContainer _injector;
        private static IScheduler _scheduler;

        private static async Task Main(string[] args)
        {
            Console.WriteLine("Starting background services");

            // Read appsettings.json and prepare dependency injector
            _settings = ReadSettings();
            _injector = DependencyResolver.BuildDependencyResolver(_settings);

            // Get Quartz job scheduler ready
            StdSchedulerFactory factory = new StdSchedulerFactory();
            _scheduler = await factory.GetScheduler();
            await _scheduler.Start();

            // Launch job(s)
            // Add new jobs here
            var scheduledJobs = new List<(IJobDetail Job, ITrigger Trigger)>
            {
                QuartzJobBuilder.GetHelloWorldJob(_injector, _settings)
            };

            scheduledJobs.ForEach(async x => await _scheduler.ScheduleJob(x.Job, x.Trigger));

            // Cleanup
            Console.WriteLine("Press ENTER to exit");
            Console.ReadLine();
            Console.WriteLine("Stopping background services");
            await _scheduler.Shutdown();
        }

        private static ApplicationSettings ReadSettings()
        {
            var settings = new ApplicationSettings();
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .Build();
            configuration.GetSection("ApplicationSettings").Bind(settings);
            return settings;
        }
    }
}
