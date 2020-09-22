using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SimpleSocial.Data.Common.Constants;
using SimpleSocial.Data.Models;
using SimpleSocial.Data.Seeding;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleSocial.Data.Seeding
{
    public class RolesSeeder : ISeeder
    {     
        public async Task SeedAsync(SimpleSocialContext dbContext, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<SimpleSocialUser>>();

            await SeedRoleAsync(roleManager, AdminConstants.AdminRoleName);
            await SeedRoleAsync(roleManager, GlobalConstants.NormalUserRoleName);
            await SeedAdminUserAsync(userManager);
        }

        private async Task SeedRoleAsync(RoleManager<ApplicationRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                var result = await roleManager.CreateAsync(new ApplicationRole(roleName));
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }
            }
        }
        private async Task SeedAdminUserAsync(UserManager<SimpleSocialUser> userManager)
        {
            var user = new SimpleSocialUser()
            {
                UserName = AdminConstants.AdminUsername,
                Email = AdminConstants.AdminEmail,
            };

            var result = await userManager.CreateAsync(user, AdminConstants.AdminPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, AdminConstants.AdminRoleName);
            }
        }
    }
}
