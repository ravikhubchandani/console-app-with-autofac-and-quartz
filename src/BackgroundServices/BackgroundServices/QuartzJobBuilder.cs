using Autofac;
using BackgroundServices.Jobs;
using Microsoft.Extensions.Logging;
using Quartz;
using System;

namespace BackgroundServices
{
    public static class QuartzJobBuilder
    {
        public static (IJobDetail Job, ITrigger Trigger) GetHelloWorldJob(IContainer injector, ApplicationSettings settings)
        {
            JobDataMap data = new JobDataMap();
            using (var scope = injector.BeginLifetimeScope())
            {
                data.Add("logger", scope.Resolve<ILogger<HelloWorldService>>());
            }
            return GetMappedJob<HelloWorldService>(data, HelloWorldService.ServiceName, settings.HelloWorldTimer);
        }

        private static (IJobDetail Job, ITrigger Trigger) GetMappedJob<T>(JobDataMap data, string serviceName, string serviceSchedule) where T : IJob
        {
            ITrigger trigger = TriggerBuilder.Create().WithSchedule(CronScheduleBuilder.CronSchedule(serviceSchedule)).Build();

            IJobDetail jobDetail =
                JobBuilder.Create<T>()
                .WithIdentity(Guid.NewGuid().ToString(), serviceName)
                .UsingJobData(data)
                .Build();

            return (jobDetail, trigger);

        }
    }
}
