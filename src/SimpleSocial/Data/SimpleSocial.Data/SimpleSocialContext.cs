﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleSocial.Data.Models;

namespace SimpleSocial.Data
{
    public class SimpleSocialContext : 
        IdentityDbContext<
                        SimpleSocialUser,
                        ApplicationRole, 
                        int, 
                        IdentityUserClaim<int>,
                        UserApplicationRole,
                        IdentityUserLogin<int>,
                        IdentityRoleClaim<int>,
                        IdentityUserToken<int>>
    {
        public SimpleSocialContext(DbContextOptions<SimpleSocialContext> options)
            : base(options)
        {
        }

        public DbSet<UserFollower> UserFollowers { get; set; }

        public DbSet<PostReport> PostReports { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Page> Pages { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<UserLike> UserLikes { get; set; }
       
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<SimpleSocialUser>()
               .HasMany(u => u.Comments)
               .WithOne(c => c.Author)
               .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserFollower>()
                .HasKey(uf => new { uf.UserId, uf.FollowerId });

            builder.Entity<UserFollower>()
                .HasOne(uf => uf.User)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserFollower>()
                .HasOne(uf => uf.Follower)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserLike>()
                .HasKey(ul => new { ul.UserId, ul.PostId });

            builder.Entity<Post>()
                .HasMany(p => p.PostReports)
                .WithOne(pr => pr.Post)
                .HasForeignKey(pr => pr.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Post>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Post)
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<Post>()
                .HasMany(p => p.Likes)
                .WithOne(l => l.Post)
                .HasForeignKey(l => l.PostId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<PostReport>()
                .HasOne(pr => pr.Post)
                .WithMany(p => p.PostReports)
                .HasForeignKey(pr => pr.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.ConfigureIdentityTables();
        }
    }
}
