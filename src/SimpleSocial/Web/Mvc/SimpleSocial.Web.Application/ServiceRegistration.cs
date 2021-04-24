using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SimpleSocial.Web.Application.Common;
using SimpleSocial.Web.Application.Common.PipelineBehaviors;
using System.Reflection;

namespace SimpleSocial.Web.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            return services;
        }
    }
}
