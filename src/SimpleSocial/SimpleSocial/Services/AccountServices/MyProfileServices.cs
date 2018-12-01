using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SimpleSocial.Data;
using SimpleSocial.Models;

namespace SimpleSocial.Services.AccountServices
{
    public class MyProfileServices : IMyProfileServices
    {
        private readonly ApplicationDbContext db;

        public MyProfileServices(ApplicationDbContext db)
        {
            this.db = db;
        }

        public ICollection<Post> GetUserPosts(string userId)
        {
            var posts = this.db.Posts.Where(x => x.UserId == userId).ToArray();

            return posts;
        }

        public string GetWallId(string userId)
        {
            
            var posts = db.Walls.FirstOrDefault(w => w.UserId == userId)?.Id;

            return posts;
        }
    }

}
