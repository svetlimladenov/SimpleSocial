using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleSocial.Data;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.PostsServices;

namespace SandBox
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine($"{typeof(Program).Namespace} ({string.Join(" ", args)}) starts working...");
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider(true);

            using (var serviceScope = serviceProvider.CreateScope())
            {
                serviceProvider = serviceScope.ServiceProvider;
                SandboxCode(serviceProvider);
            }
        }

        private static void SandboxCode(IServiceProvider serviceProvider)
        {
            var userRepository = serviceProvider.GetService<IRepository<SimpleSocialUser>>();
            var contents = new List<string>()
            {
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec efficitur leo. Etiam egestas, erat et ornare consectetur, ligula arcu tincidunt elit, non eleifend diam augue sed arcu. Vestibulum laoreet ultrices purus, id lacinia ligula accumsan non. Fusce tempor eget lorem hendrerit vehicula. Aliquam malesuada purus ac sodales pulvinar. Quisque eleifend odio id ipsum dictum, quis tristique enim maximus. Sed a pretium odio. Vestibulum ante ipsum primis in faucibus orci luctus et ultrice",
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                "Kur",
                "Chestita nova godina",
                "Oro kurwi",
            };
            for (int i = 0; i < 10; i++)
            {
                foreach (var user in userRepository.All().Include(x => x.Wall))
                {
                    var rnd = new Random();
                    var randomPost = rnd.Next(0, contents.Count - 1);
                    user.Posts.Add(new Post()
                    {
                        UserId = user.Id,
                        WallId = user.WallId,
                        Content = contents[randomPost],
                    });
                }
            }

            userRepository.SaveChangesAsync().GetAwaiter().GetResult();
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
