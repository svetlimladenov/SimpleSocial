using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleSocia.Services.Models.Account;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.FollowersDataServices;
using SimpleSocial.Services.DataServices.PostsServices;
using SimpleSocial.Services.DataServices.UsersDataServices;

namespace SimpleSocial.Web.Controllers
{
    public class ProfilesController : BaseController
    {
        private readonly UserManager<SimpleSocialUser> userManager;
        private readonly IUserServices userServices;
        private readonly IPostServices postServices;
        private readonly IFollowersServices followersServices;

        public ProfilesController(
            UserManager<SimpleSocialUser> userManager,
            IUserServices userServices,
            IPostServices postServices,
            IFollowersServices followersServices)
        {
            this.userManager = userManager;
            this.userServices = userServices;
            this.postServices = postServices;
            this.followersServices = followersServices;
        }

        public IActionResult Index(string userId)
        {
            if (this.userManager.GetUserId(User) == userId)
            {
                return RedirectToAction("MyProfile", "Account");
            }

            var currentUserId = this.userManager.GetUserId(User);

            var viewModel = new PostsFeedAndUserInfoViewModel()
            {
                CurrentUserInfo = userServices.GetUserInfo(currentUserId, currentUserId),
                Posts = postServices.GetUserPosts(userId, currentUserId),
                UserProfileInfo = userServices.GetUserInfo(userId, currentUserId),
            };
            
            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult SuccessfullInput()
        {
            return View();
        }
    }
}