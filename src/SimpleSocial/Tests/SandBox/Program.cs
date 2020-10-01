using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleSocial.Data;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;

namespace SandBox
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine($"{typeof(Program).Namespace} ({string.Join(" ", args)}) starts working...");
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider(true);

            using (var serviceScope = serviceProvider.CreateScope())
            {
                serviceProvider = serviceScope.ServiceProvider;
                await SandboxCode(serviceProvider);
            }
        }

        private static async Task SandboxCode(IServiceProvider serviceProvider)
        {
            var userRepo = serviceProvider.GetService<IRepository<SimpleSocialUser>>();
            var postRepo = serviceProvider.GetService<IRepository<Post>>();

            var dbContext = serviceProvider.GetService<SimpleSocialContext>();

            var posts = await dbContext.Users.ToListAsync();

            var a = 1;
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

            services.AddDbContext<SimpleSocialContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IRepository<>), typeof(DbRepository<>));
        }

    }
}
