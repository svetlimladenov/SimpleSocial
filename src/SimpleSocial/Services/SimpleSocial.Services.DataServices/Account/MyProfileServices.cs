using System.Linq;
using System.Security.Claims;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Identity;
using SimpleSocia.Services.Models.Account;
using SimpleSocia.Services.Models.Users;
using SimpleSocial.Data;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.ProfilePictureServices;

namespace SimpleSocial.Services.DataServices.Account
{
    public class MyProfileServices : IMyProfileServices
    {
        private readonly IProfilePictureService profilePictureService;
        private readonly SimpleSocialContext dbContext;
        private readonly UserManager<SimpleSocialUser> userManager;

        public MyProfileServices(
            IProfilePictureService profilePictureService,
            SimpleSocialContext dbContext,
            UserManager<SimpleSocialUser> userManager
            )
        {
            this.profilePictureService = profilePictureService;
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public UserInfoViewModel GetUserInfo(string userId)
        {
            var user = this.dbContext.Users.FirstOrDefault(x => x.Id == userId);
            if (user == null)
            {
                return null;
            }

            var userInfo = new UserInfoViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                ProfilePictureURL = this.profilePictureService.GetUserProfilePictureURL(userId),
                WallId = user.WallId,
            };

            return userInfo;
        }
    }
}
