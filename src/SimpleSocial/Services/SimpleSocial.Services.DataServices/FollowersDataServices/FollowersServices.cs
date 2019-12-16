using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleSocial.Services.Mapping;
using SimpleUserViewModel = SimpleSocia.Services.Models.Followers.SimpleUserViewModel;
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

        public IEnumerable<SimpleUserViewModel> GetUsersToFollow(ClaimsPrincipal user)
        {
            var currentUser = userManager.GetUserAsync(user).GetAwaiter().GetResult();
            var allUsers = this.userRepository.All().Include(x => x.ProfilePicture).Where(x => x.Id != currentUser.Id).To<SimpleUserViewModel>().Take(20).ToList();

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

        public async Task Follow(string userToFollowId, string currentUserId)
        {
            var userFollower = new UserFollower()
            {
                UserId = userToFollowId,
                FollowerId = currentUserId,
            };

            if (this.IsBeingFollowedBy(userToFollowId, currentUserId))
            {
                return;
            }

            await userFollowerRepository.AddAsync(userFollower);
            userFollowerRepository.SaveChangesAsync().GetAwaiter().GetResult();
        }

        public async Task Unfollow(string userToUnfollowId, string currentUserId)
        {
            if (!this.IsBeingFollowedBy(userToUnfollowId, currentUserId))
            {
                return;
            }
            
            var userFollower = userFollowerRepository.All().FirstOrDefault(x => x.UserId == userToUnfollowId && x.FollowerId == currentUserId);
            if (userFollower == null)
            {
                return;
            }

            userFollowerRepository.Delete(userFollower);
            userFollowerRepository.SaveChangesAsync().GetAwaiter().GetResult();
        }

        public ICollection<SimpleUserViewModel> GetFollowings(ClaimsPrincipal user)
        {
            var userId = this.userManager.GetUserId(user);
            var followings = this.userFollowerRepository.All().Include(x => x.User).Where(x => x.FollowerId == userId && x.UserId != userId)
                .Select(
                    x => x.User).To<SimpleUserViewModel>().ToList();

            foreach (var following in followings)
            {
                following.IsFollowingCurrentUser = this.IsBeingFollowedBy(following.Id, userId);
            }

            return followings;
        }


        public ICollection<SimpleUserViewModel> GetFollowers(ClaimsPrincipal user)
        {
            var userId = this.userManager.GetUserId(user);
            var followers = this.userFollowerRepository.All().Include(x => x.Follower).Where(x => x.UserId == userId && x.FollowerId != userId).Select(x => x.Follower).To<SimpleUserViewModel>().ToList();
            foreach (var follower in followers)
            {
                follower.IsFollowingCurrentUser = this.IsBeingFollowedBy(follower.Id, userId);
            }

            return followers;
        }

        //is user A followed by user B
        public bool IsBeingFollowedBy(string userA, string userB)
        {
            var userAid = userA;
            var userBid = userB;
            return this.userFollowerRepository.All().FirstOrDefault(x => x.UserId == userAid && x.FollowerId == userBid) != null;
        }
    }
}