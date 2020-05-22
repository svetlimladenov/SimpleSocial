using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SimpleSocial.Data;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.FollowersDataServices;
using SimpleSocial.Services.DataServices.ProfilePictureServices;
using SimpleSocial.Services.Models;
using SimpleSocial.Services.Models.Users;

namespace SimpleSocial.Services.DataServices.UsersDataServices
{
    public class UserServices : IUserServices
    {
        private readonly SimpleSocialContext dbContext;
        private readonly IMapper mapper;
        private readonly IFollowersServices followersServices;
        private readonly IProfilePictureService profilePictureService;
        private readonly UserManager<SimpleSocialUser> userManager;

        public UserServices(
            SimpleSocialContext dbContext, 
            IMapper mapper,         
            IFollowersServices followersServices,
            IProfilePictureService profilePictureService,
            UserManager<SimpleSocialUser> userManager)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.followersServices = followersServices;
            this.profilePictureService = profilePictureService;
            this.userManager = userManager;
        }

        //TODO: Remove this method and dont use it
        public ICollection<string> GetAllUsernames()
         => this.dbContext.Users.Select(x => x.UserName).ToList();


        public UserInfoViewModel GetUserInfo(string userId, string currentUserId)
        {
            var user = this.dbContext.Users.FirstOrDefault(x => x.Id == userId);
            if (user == null)
            {
                return null;
            }

            var userInfo = mapper.Map<SimpleSocialUser, UserInfoViewModel>(user);
            userInfo.ProfilePictureURL = this.profilePictureService.GetUserProfilePictureURL(userId);
            //is user A followed by user B
            var isBeingFollowedByCurrentUser = this.followersServices.IsBeingFollowedBy(userId, currentUserId) || userId == currentUserId;
            var userFollowers = this.dbContext.UserFollowers.Count(x => x.UserId == user.Id);
            var userFollowings = this.dbContext.UserFollowers.Count(x => x.FollowerId == user.Id);

            userInfo.Age = user.BirthDay.GetAge();
            userInfo.IsBeingFollowedByCurrentUser = isBeingFollowedByCurrentUser;
            userInfo.FollowersCount = userFollowers;
            userInfo.FollowingsCount = userFollowings;
            return userInfo;
        }
    }
}