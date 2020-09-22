using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SimpleSocial.Data.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleSocial.Services.Mapping;
using SimpleSocial.Data;
using SimpleSocial.Services.Models.Followers;

namespace SimpleSocial.Services.DataServices.FollowersDataServices
{
    public class FollowersServices : IFollowersServices
    {
        private readonly UserManager<SimpleSocialUser> userManager;
        private readonly SimpleSocialContext dbContext;

        public FollowersServices(
            UserManager<SimpleSocialUser> userManager,
            SimpleSocialContext dbContext)
        {
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<T>> GetUsersToFollow<T>(ClaimsPrincipal user)
        {
            var currentUser = await userManager.GetUserAsync(user);
            var notFollowingUsers =
                this.dbContext.Users
                .Where(
                    u => !this.dbContext.UserFollowers
                    .Where(x => x.FollowerId == currentUser.Id)
                    .Select(x => x.UserId)
                    .Contains(u.Id)
                    && u.Id != currentUser.Id)
                .MapToList<T>();

            return notFollowingUsers;
        }

        public async Task<IEnumerable<SimpleUserViewModel>> GetUsersToFollow(ClaimsPrincipal user)
        {
            var currentUser = await userManager.GetUserAsync(user);
            var notFollowingUsers = 
                this.dbContext.Users
                .Where(
                    u => !this.dbContext.UserFollowers
                    .Where(x => x.FollowerId == currentUser.Id)
                    .Select(x => x.UserId)
                    .Contains(u.Id) 
                    && u.Id != currentUser.Id)
                .MapToList<SimpleUserViewModel>();

            return notFollowingUsers;
        }

        public async Task Follow(int userToFollowId, int currentUserId)
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

            await this.dbContext.UserFollowers.AddAsync(userFollower);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task Unfollow(int userToUnfollowId, int currentUserId)
        {
            if (!this.IsBeingFollowedBy(userToUnfollowId, currentUserId))
            {
                return;
            }
            
            var userFollower = this.dbContext.UserFollowers.FirstOrDefault(x => x.UserId == userToUnfollowId && x.FollowerId == currentUserId);
            if (userFollower == null)
            {
                return;
            }

            this.dbContext.UserFollowers.Remove(userFollower);
            await this.dbContext.SaveChangesAsync();
        }

        public ICollection<SimpleUserViewModel> GetFollowings(ClaimsPrincipal user)
        {
            var userId = int.Parse(this.userManager.GetUserId(user));
            var followings = this.dbContext.UserFollowers
                .Where(x => x.FollowerId == userId && x.UserId != userId)
                .Select(x => x.User)
                .MapToList<SimpleUserViewModel>();

            // TODO: DO this in the database
            foreach (var following in followings)
            {
                following.IsFollowingCurrentUser = this.IsBeingFollowedBy(following.Id, userId);
            }

            return followings;
        }


        public ICollection<SimpleUserViewModel> GetFollowers(ClaimsPrincipal user)
        {
            var userId = int.Parse(this.userManager.GetUserId(user));
            var followers = this.dbContext.UserFollowers
                .Where(x => x.UserId == userId && x.FollowerId != userId)
                .Select(x => x.Follower)
                .MapToList<SimpleUserViewModel>();

            // TODO: DO this in the database
            foreach (var follower in followers)
            {
                follower.IsFollowingCurrentUser = this.IsBeingFollowedBy(follower.Id, userId);
            }

            return followers;
        }

        /// <summary>
        /// Checks if user A is being followed by user B
        /// </summary>
        /// <param name="userA"></param>
        /// <param name="userB"></param>
        /// <returns></returns>
        public bool IsBeingFollowedBy(int userA, int userB)
        {
            //TODO: Fix this
            var userAid = userA;
            var userBid = userB;
            return this.dbContext.UserFollowers.FirstOrDefault(x => x.UserId == userAid && x.FollowerId == userBid) != null;
        }
    }
}