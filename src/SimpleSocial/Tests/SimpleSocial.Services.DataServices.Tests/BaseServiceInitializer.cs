using System;
using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SimpleSocial.Data;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.Account;
using SimpleSocial.Services.DataServices.CommentsServices;
using SimpleSocial.Services.DataServices.FollowersDataServices;
using SimpleSocial.Services.DataServices.LikesDataServices;
using SimpleSocial.Services.DataServices.PostsServices;
using SimpleSocial.Services.DataServices.ReportsDataServices;
using SimpleSocial.Services.DataServices.SearchDataServices;
using SimpleSocial.Services.DataServices.SignUpDetails;
using SimpleSocial.Services.DataServices.UsersDataServices;
using SimpleSocial.Web.Utilities;

namespace SimpleSocial.Services.DataServices.Tests
{
    public class BaseServiceInitializer : IDisposable
    {
        protected IServiceProvider Provider { get; set; }

        protected SimpleSocialContext Context { get; set; }

        public static object thisLock = new object();

        public BaseServiceInitializer()
        {
            lock (thisLock)
            {
                //Mapper.Reset();
                //Mapper.Initialize(x => { x.AddProfile<SimpleSocialProfile>(); });
            }
            

            var services = SetServices();
            this.Provider = services.BuildServiceProvider();
            this.Context = this.Provider.GetRequiredService<SimpleSocialContext>();
            SetScoppedServiceProvider();
        }


        private void SetScoppedServiceProvider()
        {
            var httpContext = this.Provider.GetService<IHttpContextAccessor>();
            httpContext.HttpContext.RequestServices = this.Provider.CreateScope().ServiceProvider;
        }

        public void Dispose()
        {
            this.Context.Database.EnsureDeleted();
        }

        private ServiceCollection SetServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<SimpleSocialContext>(
                opt => opt.UseInMemoryDatabase(Guid.NewGuid()
                    .ToString()));

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped(typeof(IRepository<>), typeof(DbRepository<>));
            services.AddScoped<IMyProfileServices, MyProfileServices>();
            services.AddScoped<IPostServices, PostServices>();
            services.AddScoped<ICommentsServices, CommentsServices.CommentsServices>();
            services.AddScoped<IProfileDetailsServices, ProfileDetailsServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddTransient<ILikesServices, LikesServices>();
            services.AddScoped<IFollowersServices, FollowersServices>();
            services.AddScoped<ISearchServices, SearchServices>();
            services.AddScoped<IReportsService, ReportsService>();

            services.AddIdentity<SimpleSocialUser, IdentityRole>(opt =>
                {
                    opt.Password.RequireDigit = false;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequiredLength = 3;
                    opt.Password.RequiredUniqueChars = 0;
                })
                .AddEntityFrameworkStores<SimpleSocialContext>()
                .AddDefaultTokenProviders();

            var context = new DefaultHttpContext();

            services.AddSingleton<IHttpContextAccessor>(
                new HttpContextAccessor()
                {
                    HttpContext = context,
                });
            return services;
        }
    }
}
