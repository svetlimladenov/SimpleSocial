using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleSocial.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleSocial.Data
{
    public static class ModelBuilderExtensions
    {
        public static void ConfigureIdentityTables(this ModelBuilder builder)
        {
            builder.Entity<SimpleSocialUser>().ToTable("Users", "identity");

            builder.Entity<ApplicationRole>().ToTable("Roles", "identity");

            builder.Entity<UserApplicationRole>().ToTable("UsersRole", "identity");

            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaim", "identity");

            builder.Entity<IdentityRoleClaim<int>>().ToTable("RoleClaim", "identity");

            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogin", "identity");

            builder.Entity<IdentityUserToken<int>>().ToTable("UserToken", "identity");
        }
    }
}
