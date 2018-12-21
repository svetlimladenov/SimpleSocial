using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleSocial.Data.Models;

namespace SimpleSocial.Data
{
    public class SimpleSocialContext : IdentityDbContext<SimpleSocialUser>
    {
        public SimpleSocialContext(DbContextOptions<SimpleSocialContext> options)
            : base(options)
        {
        }

        public DbSet<UserFollower> UserFollowers { get; set; }

        public DbSet<PostReport> PostReports { get; set; }

        public DbSet<Wall> Walls { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Page> Pages { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<ProfilePicture> ProfilePictures { get; set; }

       
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<SimpleSocialUser>()
                .HasOne(u => u.Wall)
                .WithOne(w => w.User)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<SimpleSocialUser>()
                .HasMany(u => u.Comments)
                .WithOne(c => c.Author)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UserFollower>()
                .HasKey(ur => new { ur.UserId, ur.FollowerId });

            builder.Entity<SimpleSocialUser>()
                .HasMany(u => u.UserFollowers)
                .WithOne(u => u.Follower)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<PostReport>()
                .HasOne(pr => pr.Post)
                .WithMany(p => p.PostReports)
                .HasForeignKey(pr => pr.PostId);

            builder.Entity<SimpleSocialUser>()
                .HasOne(u => u.ProfilePicture)
                .WithOne(p => p.User)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
