using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleSocial.Models;
using SimpleSocial.Services.AccountServices;
using SimpleSocial.ViewModels.Account;

namespace SimpleSocial.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IMyProfileServices myProfileServices;
        private readonly UserManager<User> userManager;

        public AccountController(
            IMyProfileServices myProfileServices,
            UserManager<User> userManager)
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
