using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleSocia.Services.Models.Account;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices;
using SimpleSocial.Services.DataServices.Account;

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
            viewModel.Posts = myProfileServices.GetUserPosts(User);
            viewModel.WallId = myProfileServices.GetWallId(User);
            viewModel.UserId = userManager.GetUserId(User);
            return View(viewModel);
        }
    }
}
