using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleSocial.Models;

namespace SimpleSocial.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<UserFriend> UserFriends { get; set; }

        public DbSet<Wall> Walls { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Page> Pages { get; set; }

        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=.\\SQLEXPRESS;Database=SimpleSocial;Trusted_Connection=True;MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>()
                .HasOne(u => u.Wall)
                .WithOne(w => w.User)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>()
                .HasMany(u => u.Comments)
                .WithOne(c => c.Author)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserFriend>()
                .HasKey(ur => new { ur.UserId, ur.FriendId });

            builder.Entity<User>()
                .HasMany(u => u.UserFriends)
                .WithOne(u => u.Friend)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
