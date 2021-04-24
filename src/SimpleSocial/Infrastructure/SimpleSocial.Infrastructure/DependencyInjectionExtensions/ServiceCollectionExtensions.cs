using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog.Extensions.Logging;
using SimpleSocial.Common.Logging;
using SimpleSocial.Infrastructure.Logging;
using LoggerFactory = SimpleSocial.Infrastructure.Logging.LoggerFactory;

namespace SimpleSocial.Infrastructure.DependencyInjectionExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterLogger(this IServiceCollection services, IConfiguration configuration)
        {
            var logger = LoggerFactory.CreateLogger(configuration);
            services.AddSingleton<ILoggerFactory>(services => new SerilogLoggerFactory(logger, true));
            services.AddSingleton(typeof(ISimpleSocialLogger<>), typeof(SimpleSocialLogger<>));

            return services;
        }
    }
}
