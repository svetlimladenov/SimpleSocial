using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using SimpleSocial.Web.Utilities;

namespace SimpleSocial.Web.Middlewares
{
    public class SeedRolesMiddleware
    {
        private readonly RequestDelegate next;

        public SeedRolesMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext, RoleManager<IdentityRole> roleManager)
        {
            await Seeder.SeedRoles(roleManager);

            await this.next(httpContext);
        }
    }
}
