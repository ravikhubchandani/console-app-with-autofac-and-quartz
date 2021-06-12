using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BackgroundServices
{
    public static class DependencyResolver
    {
        public static IContainer BuildDependencyResolver(ApplicationSettings settings)
        {
            var injector = new ContainerBuilder();
            injector.Populate(ConfigureServices(settings));
            return injector.Build();
        }

        private static IServiceCollection ConfigureServices(ApplicationSettings settings)
        {
            var svc = new ServiceCollection();
            svc.AddLogging(logs => logs.AddConsole());
            return svc;
        }
    }
}
