using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SimpleSocial.Common
{
    public class Seeder
    {
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            bool adminRoleExists = await roleManager.RoleExistsAsync("Admin");
            if (!adminRoleExists)
            {
                var adminRole = new IdentityRole() { Name = "Admin" };
                await roleManager.CreateAsync(adminRole);
            }

            bool userRoleExists = await roleManager.RoleExistsAsync("User");
            if (!userRoleExists)
            {
                var userRole = new IdentityRole() { Name = "User"};
                await roleManager.CreateAsync(userRole);
            }
        }
    }
}
