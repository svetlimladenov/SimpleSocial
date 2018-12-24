using System;
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
            var postServices = serviceProvider.GetService<IRepository<Post>>();
            var usersRepository = serviceProvider.GetService<IRepository<SimpleSocialUser>>();
            var likesRepository = serviceProvider.GetService<IRepository<UserLike>>();


            var post = postServices.All().FirstOrDefault();
            var like = new UserLike()
            {
                PostId = "ee7a8a57-87fd-423c-9f8a-7c218c0abf1d",
                UserId = "374a0abb-ca44-4a7a-9a3f-66d11ba7b9bb"
            };

            foreach (var user in usersRepository.All().ToList())
            {
                user.Likes.Add(new UserLike() {PostId = "b9249440-54d0-4e49-bcc6-4c55c24d586b",UserId = user.Id});
            }

            usersRepository.SaveChangesAsync().GetAwaiter().GetResult();
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
