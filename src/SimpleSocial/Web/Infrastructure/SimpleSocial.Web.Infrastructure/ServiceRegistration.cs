using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleSocial.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using SimpleSocial.Application.Repositories;
using SimpleSocial.Infrastructure.Persistance.Repositories;

namespace SimpleSocial.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
               .AddDbContext<WebDbContext>(options => options
                   .UseSqlServer(
                       configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<IUserReadonlyRepository, UserReadonlyRepository>();
            
            return services;
        }
    }
}
