using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleSocial.Data;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.FollowersDataServices;
using SimpleSocial.Services.Models;
using SimpleSocial.Services.Models.Users;

namespace SimpleSocial.Services.DataServices.UsersDataServices
{
    public class UserServices : IUserServices
    {
        private readonly SimpleSocialContext dbContext;
        private readonly IMapper mapper;
        private readonly IFollowersServices followersServices;
        private readonly UserManager<SimpleSocialUser> userManager;

        public UserServices(
            SimpleSocialContext dbContext, 
            IMapper mapper,         
            IFollowersServices followersServices,
            UserManager<SimpleSocialUser> userManager)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.followersServices = followersServices;
            this.userManager = userManager;
        }

        //TODO: Remove this method and dont use it
        public ICollection<string> GetAllUsernames()
         => this.dbContext.Users.Select(x => x.UserName).ToList();

        public int GetUserId(ClaimsPrincipal claimsPrincipal)
        {
            var userId = this.userManager.GetUserId(claimsPrincipal);
            return int.TryParse(userId, out int id) ? id : 0;
        }

        public async Task<UserInfoViewModel> GetUserInfo(int userId, int currentUserId)
        {
            var user = this.dbContext.Users.Find(userId);

            var userInfo = mapper.Map<SimpleSocialUser, UserInfoViewModel>(user);
            var isBeingFollowedByCurrentUser = this.followersServices.IsBeingFollowedBy(userId, currentUserId) || userId == currentUserId;
            var userFollowers = await this.dbContext.UserFollowers.CountAsync(x => x.UserId == user.Id);
            var userFollowings = await this.dbContext.UserFollowers.CountAsync(x => x.FollowerId == user.Id);

            userInfo.Age = user.BirthDay.GetAge();
            userInfo.IsBeingFollowedByCurrentUser = isBeingFollowedByCurrentUser;
            userInfo.FollowersCount = userFollowers;
            userInfo.FollowingsCount = userFollowings;
            return userInfo;
        }
    }
}