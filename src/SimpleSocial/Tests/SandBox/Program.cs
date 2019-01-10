using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
            var userRepo = serviceProvider.GetService<IRepository<SimpleSocialUser>>();
            var postRepo = serviceProvider.GetService<IRepository<Post>>();

            var postContent = new List<string>()
            {
                "Happy New Year !!!",
                "Merry Christmas !!!",
                "Hi there",
                "Good Morning",
                "Good Morning, Friends",
                "Good Afternoon",
                "Good Evening",
                "Night Out",
                "I miss her",
                "Do you miss me",
                "I love music",
                "I LOVE STACKOVERFLOW",
                "I LOVE STACKOVERFLOW",
                "I LOVE STACKOVERFLOW",
                "I love dogs",
                "Supp bitches",
                "Yesterday was one of the best days in my entire life",
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus ac diam vel justo consectetur ultrices. Nulla sed sollicitudin nunc, ac porta leo. Mauris dapibus quis est in porttitor. In vestibulum pretium eros sit amet porttitor. Phasellus quis fermentum libero. Curabitur egestas risus imperdiet molestie tincidunt. Suspendisse potenti. Donec suscipit imperdiet lacinia. Donec placerat dictum efficitur.",
                "Vivamus non justo quis dolor iaculis vehicula nec sed diam. Nam placerat nibh eu luctus mattis. Vestibulum pulvinar purus sed scelerisque blandit. Nullam nec mauris ex. Vestibulum mauris lorem,"
            };
            var rnd = new Random();
            foreach (var user in userRepo.All().Include(x => x.Wall))
            {
                for (int i = 0; i < rnd.Next(10, 20); i++)
                {
                    var post = new Post
                    {
                        Content = postContent[rnd.Next(0, postContent.Count - 1)],
                        UserId = user.Id,
                        User = user,
                        WallId = user.WallId,
                    };
                    postRepo.AddAsync(post).GetAwaiter().GetResult();
                    
                }
            }
            postRepo.SaveChangesAsync().GetAwaiter().GetResult();
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
