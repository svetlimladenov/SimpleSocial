using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;

namespace SimpleSocial.Services.DataServices
{
    public class MyProfileServices : IMyProfileServices
    {
        private readonly IRepository<Post> postRepository;
        private readonly IRepository<Wall> wallRepository;
        private readonly UserManager<SimpleSocialUser> userManager;


        public MyProfileServices(
            IRepository<Post> postRepository,
            IRepository<Wall> wallRepository,
            UserManager<SimpleSocialUser> userManager)
        {
            this.postRepository = postRepository;
            this.wallRepository = wallRepository;
            this.userManager = userManager;
        }

        public ICollection<Post> GetUserPosts(ClaimsPrincipal user)
        {
            var userId = userManager.GetUserId(user);
            var posts = this.postRepository.All().Where(x => x.UserId == userId).ToArray();

            return posts;
        }

        public string GetWallId(ClaimsPrincipal user)
        {
            var userId = userManager.GetUserId(user);
            var posts = wallRepository.All().FirstOrDefault(w => w.UserId == userId)?.Id;

            return posts;
        }
    }
}
