using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleSocial.Data.Models;
using SimpleSocial.Web.Models;
using SimpleSocial.Web.Services;

namespace SimpleSocial.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IMyProfileServices myProfileServices;
        private readonly UserManager<SimpleSocialUser> userManager;

        public AccountController(
            IMyProfileServices myProfileServices,
            UserManager<SimpleSocialUser> userManager)
        {
            this.myProfileServices = myProfileServices;
            this.userManager = userManager;
        }

        public IActionResult MyProfile()
        {
            var viewModel = new MyProfileViewModel();
            var userId = userManager.GetUserId(User);
            viewModel.Posts = myProfileServices.GetUserPosts(userId);
            viewModel.WallId = myProfileServices.GetWallId(userId);
            viewModel.UserId = userId;
            return View(viewModel);
        }
    }
}
