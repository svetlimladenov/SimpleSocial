using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SimpleSocial.Services.Models;
using SimpleSocial.Data;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.Account;
using SimpleSocial.Services.DataServices.CommentsServices;
using SimpleSocial.Services.DataServices.FollowersDataServices;
using SimpleSocial.Services.DataServices.LikesDataServices;
using SimpleSocial.Services.DataServices.PostsServices;
using SimpleSocial.Services.DataServices.ProfilePictureServices;
using SimpleSocial.Services.DataServices.ReportsDataServices;
using SimpleSocial.Services.DataServices.SearchDataServices;
using SimpleSocial.Services.DataServices.SignUpDetails;
using SimpleSocial.Services.DataServices.UsersDataServices;
using SimpleSocial.Services.Mapping;
using SimpleSocial.Web.Areas.Administration.Services;
using System.Reflection;
using SimpleSocial.Data.Seeding;
using SimpleSocial.Application;
using SimpleSocial.Infrastructure;
using Elastic.Apm.AspNetCore;
using SimpleSocial.Web.Application;
using SimpleSocial.Common.Logging;
using SimpleSocial.Infrastructure.Logging;
using Microsoft.Extensions.Logging;
using Serilog.Extensions.Logging;
using LoggerFactory = SimpleSocial.Infrastructure.Logging.LoggerFactory;
using SimpleSocial.Infrastructure.DependencyInjectionExtensions;
using SimpleSocial.Common.Tracing;
using SimpleSocial.Infrastructure.Tracing;
using Elastic.Apm.Api;

namespace SimpleSocial.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddDbContext<SimpleSocialContext>(options =>
                options.UseSqlServer(
                    this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<SimpleSocialUser, ApplicationRole>(
                    options =>
                    {
                        options.Password.RequiredLength = 6;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequireDigit = false;
                    })
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<SimpleSocialContext>();

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin",
                    authBuilder =>
                    {
                        authBuilder.RequireRole("Admin");
                    });
            });

            //TODO: Add auto validate anti foregety token attribute
            services.AddControllersWithViews();

            services.AddRazorPages();

            services.AddSession();

            services.AddScoped(typeof(IRepository<>), typeof(DbRepository<>));
            services.AddScoped<IMyProfileServices, MyProfileServices>();
            services.AddScoped<IPostServices, PostServices>();
            services.AddScoped<ICommentsServices, CommentsServices>();
            services.AddScoped<IProfileDetailsServices, ProfileDetailsServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddTransient<ILikesServices, LikesServices>();
            services.AddScoped<IFollowersServices, FollowersServices>();
            services.AddScoped<ISearchServices, SearchServices>();
            services.AddScoped<IReportsService, ReportsService>();
            services.AddScoped<IAdministrationServices, AdministrationServices>();
            services.AddScoped<IProfilePictureService, ProfilePictureService>();

            var mapper = AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);
            services.AddSingleton(mapper);

            services.RegisterApplication();
            services.RegisterInfrastructure(Configuration);
            services.RegisterLogger(Configuration);

            services.AddSingleton(x => Elastic.Apm.Agent.Tracer);
            services.AddSingleton<ISimpleSocialTracer, SimpleSocialTracer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<SimpleSocialContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            app.UseElasticApm(Configuration); // add filter for not logging static files
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseCors(config => 
            {
                config.AllowAnyHeader();
                config.AllowAnyMethod();
                config.AllowAnyOrigin();
            });
            
            app.UseHttpsRedirection();
            app.UseCookiePolicy();
            app.UseRouting();

            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                
                endpoints.MapRazorPages();
            });
        }
    }
}
