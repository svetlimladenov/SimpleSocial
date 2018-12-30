using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleSocia.Services.Models.Account;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.PostsServices;
using SimpleSocial.Services.DataServices.UsersDataServices;

namespace SimpleSocial.Web.Controllers
{
    public class ProfilesController : BaseController
    {
        private readonly UserManager<SimpleSocialUser> userManager;
        private readonly IUserServices userServices;
        private readonly IPostServices postServices;

        public ProfilesController(
            UserManager<SimpleSocialUser> userManager,
            IUserServices userServices,
            IPostServices postServices)
        {
            this.userManager = userManager;
            this.userServices = userServices;
            this.postServices = postServices;
        }

        public IActionResult Index(string userId)
        {
            if (this.userManager.GetUserId(User) == userId)
            {
                return RedirectToAction("MyProfile", "Account");
            }

            var currentUserId = this.userManager.GetUserId(User);

            var viewModel = new UserProfileViewModel
            {
                CurrentUserInfo = userServices.GetUserInfo(currentUserId),
                Posts = postServices.GetUserPosts(userId, currentUserId),
                UserProfileInfo = userServices.GetUserInfo(userId),
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