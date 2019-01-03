using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SimpleSocia.Services.Models.Users;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.Account;
using SimpleSocial.Services.DataServices.FollowersDataServices;

namespace SimpleSocial.Services.DataServices.UsersDataServices
{
    public class UserServices : IUserServices
    {
        private readonly IRepository<SimpleSocialUser> userRepository;
        private readonly IRepository<Wall> wallRepository;
        private readonly IRepository<ProfilePicture> profilePicturesRepository;
        private readonly IFollowersServices followersServices;
        private readonly UserManager<SimpleSocialUser> userManager;

        public UserServices(
            IRepository<SimpleSocialUser> userRepository,
            IRepository<Wall> wallRepository,
            IRepository<ProfilePicture> profilePicturesRepository,
            IFollowersServices followersServices,
            UserManager<SimpleSocialUser> userManager)
        {
            this.userRepository = userRepository;
            this.wallRepository = wallRepository;
            this.profilePicturesRepository = profilePicturesRepository;
            this.followersServices = followersServices;
            this.userManager = userManager;
        }

        public ICollection<string> GetAllUsernames()
        {
            return this.userRepository.All().Select(x => x.UserName).ToList();
        }


        public UserInfoViewModel GetUserInfo(string userId, string currentUserId)
        {
            var user = this.userRepository.All().FirstOrDefault(x => x.Id == userId);
            if (user == null)
            {
                return null;
            }
            //is user A followed by user B
            var isBeingFollowedByCurrentUser = this.followersServices.IsBeingFollowedBy(userId, currentUserId) || userId == currentUserId;

            var userInfo = new UserInfoViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                ProfilePicture = this.GetUserProfilePicture(userId),
                WallId = user.WallId,
                IsBeingFollowedByCurrentUser = isBeingFollowedByCurrentUser
            };

            return userInfo;
        }

        public ProfilePicture GetUserProfilePicture(string userId)
        {
            var profilePicture = profilePicturesRepository.All().FirstOrDefault(x => x.UserId == userId);
            return profilePicture;
        }

        public string GetWallId(string userId)
        {
            var posts = wallRepository.All().FirstOrDefault(w => w.UserId == userId)?.Id;
            return posts;
        }
    }
}