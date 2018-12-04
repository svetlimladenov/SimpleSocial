using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;

namespace SimpleSocial.Web.Services
{
    public class MyProfileServices : IMyProfileServices
    {
        private readonly IRepository<Post> postRepository;
        private readonly IRepository<Wall> wallRepository;

        public MyProfileServices(IRepository<Post> postRepository, IRepository<Wall> wallRepository)
        {
            this.postRepository = postRepository;
            this.wallRepository = wallRepository;
        }

        public ICollection<Post> GetUserPosts(string userId)
        {
            var posts = this.postRepository.All().Where(x => x.UserId == userId).ToArray();

            return posts;
        }

        public string GetWallId(string userId)
        {
            var posts = wallRepository.All().FirstOrDefault(w => w.UserId == userId)?.Id;
            return posts;
        }
    }
}
