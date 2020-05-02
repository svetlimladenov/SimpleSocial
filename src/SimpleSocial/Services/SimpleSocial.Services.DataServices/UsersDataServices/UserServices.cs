using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleSocia.Services.Models;
using SimpleSocia.Services.Models.Users;
using SimpleSocial.Data;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.FollowersDataServices;
using SimpleSocial.Services.DataServices.ProfilePictureServices;

namespace SimpleSocial.Services.DataServices.UsersDataServices
{
    public class UserServices : IUserServices
    {
        private readonly SimpleSocialContext dbContext;
        private readonly IRepository<SimpleSocialUser> userRepository;
        private readonly IRepository<Wall> wallRepository;
        private readonly IRepository<ProfilePicture> profilePicturesRepository;
        private readonly IRepository<UserFollower> userFollowersRepository;
        private readonly IFollowersServices followersServices;
        private readonly IProfilePictureService profilePictureService;
        private readonly UserManager<SimpleSocialUser> userManager;

        public UserServices(
            SimpleSocialContext dbContext, 
            IRepository<SimpleSocialUser> userRepository,
            IRepository<Wall> wallRepository,
            IRepository<ProfilePicture> profilePicturesRepository,
            IRepository<UserFollower> userFollowersRepository,
            IFollowersServices followersServices,
            IProfilePictureService profilePictureService,
            UserManager<SimpleSocialUser> userManager)
        {
            this.dbContext = dbContext;
            this.userRepository = userRepository;
            this.wallRepository = wallRepository;
            this.profilePicturesRepository = profilePicturesRepository;
            this.userFollowersRepository = userFollowersRepository;
            this.followersServices = followersServices;
            this.profilePictureService = profilePictureService;
            this.userManager = userManager;
        }

        //TODO: Remove this method and dont use it
        public ICollection<string> GetAllUsernames()
        {
            return this.userRepository.All().Select(x => x.UserName).ToList();
        }


        public UserInfoViewModel GetUserInfo(string userId, string currentUserId)
        {
            var user = this.dbContext.Users.FirstOrDefault(x => x.Id == userId);
            if (user == null)
            {
                return null;
            }

            var userInfo = Mapper.Map<SimpleSocialUser, UserInfoViewModel>(user);
            userInfo.ProfilePictureURL = this.profilePictureService.GetUserProfilePictureURL(userId);
            //is user A followed by user B
            var isBeingFollowedByCurrentUser = this.followersServices.IsBeingFollowedBy(userId, currentUserId) || userId == currentUserId;
            var userFollowers = this.userFollowersRepository.All().Count(x => x.UserId == user.Id);
            var userFollowings = this.userFollowersRepository.All().Count(x => x.FollowerId == user.Id);

            userInfo.Age = user.BirthDay.GetAge();
            userInfo.IsBeingFollowedByCurrentUser = isBeingFollowedByCurrentUser;
            userInfo.FollowersCount = userFollowers;
            userInfo.FollowingsCount = userFollowings;
            return userInfo;
        }
    }
}