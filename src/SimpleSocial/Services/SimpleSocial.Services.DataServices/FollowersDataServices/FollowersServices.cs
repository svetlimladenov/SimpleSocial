using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SimpleSocia.Services.Models.Followers;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SimpleSocial.Services.Mapping;
using UserFollower = SimpleSocial.Data.Models.UserFollower;

namespace SimpleSocial.Services.DataServices.FollowersDataServices
{
    public class FollowersServices : IFollowersServices
    {
        private readonly UserManager<SimpleSocialUser> userManager;
        private readonly IRepository<SimpleSocialUser> userRepository;
        private readonly IRepository<UserFollower> userFollowerRepository;

        public FollowersServices(
            UserManager<SimpleSocialUser> userManager,
            IRepository<SimpleSocialUser> userRepository,
            IRepository<UserFollower> userFollowerRepository)
        {
            this.userManager = userManager;
            this.userRepository = userRepository;
            this.userFollowerRepository = userFollowerRepository;
        }

        public ICollection<SimpeUserViewModel> GetUsersToFollow(ClaimsPrincipal user)
        {
            var currentUser = userManager.GetUserAsync(user).GetAwaiter().GetResult();
            var allUsers = this.userRepository.All().Where(x => x.Id != currentUser.Id).To<SimpeUserViewModel>().Take(20).ToList();

            if (allUsers.Count == 0)
            {
                return null;
            }

            var currentUserFollowings = this.userFollowerRepository.All().Where(x => x.FollowerId == currentUser.Id).ToList();

            foreach (var followingUserId in currentUserFollowings)
            {
                var followingUser = userRepository.All().FirstOrDefault(x => x.Id == followingUserId.UserId);
                if (followingUser == null)
                {
                    continue;   
                }
                if (allUsers.FirstOrDefault(x => x.Id == followingUser.Id) != null)
                {
                    allUsers.RemoveAll(x => x.Id == followingUser.Id);
                    continue;
                }
            }

            

            return allUsers;
        }

        public void Follow(string userToFollowId, ClaimsPrincipal user)
        {
            var userId = userManager.GetUserId(user);
            var userFollower = new UserFollower()
            {
                UserId = userToFollowId,
                FollowerId = userId,
            };

            if (userFollowerRepository.All().Contains(userFollower))
            {
                return;
            }

            userFollowerRepository.AddAsync(userFollower).GetAwaiter().GetResult();
            userFollowerRepository.SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}