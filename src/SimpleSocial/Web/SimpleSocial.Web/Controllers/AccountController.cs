using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleSocia.Services.Models.Account;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.Account;
using SimpleSocial.Services.DataServices.PostsServices;
using SimpleSocial.Services.DataServices.UsersDataServices;

namespace SimpleSocial.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IMyProfileServices myProfileServices;
        private readonly IUserServices userServices;
        private readonly IPostServices postServices;
        private readonly UserManager<SimpleSocialUser> userManager;

        public AccountController(
            IMyProfileServices myProfileServices,
            IUserServices userServices,
            IPostServices postServices,
            UserManager<SimpleSocialUser> userManager
            )
        {
            this.myProfileServices = myProfileServices;
            this.userServices = userServices;
            this.postServices = postServices;
            this.userManager = userManager;

        }


        public IActionResult MyProfile(MyProfileViewModel inputModel)
        {
            var userId = this.userManager.GetUserId(User);
            var viewModel = new MyProfileViewModel
            {
                CurrentUserInfo = userServices.GetUserInfo(userId),
                Posts = postServices.GetUserPosts(userId, userId),
                IsValidProfilePicture = inputModel.IsValidProfilePicture,
                UserProfileInfo = userServices.GetUserInfo(userId)
            };

            return View(viewModel);
        }


        public IActionResult ChangeProfilePicture()
        {
            return this.View();
        }

        public IActionResult UploadProfilePicture(UploadProfilePictureInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ChangeProfilePicture");
            }

            if (inputModel.UploadImage != null)
            {
                var indexOfImgExtensionDot = inputModel.UploadImage.FileName.IndexOf('.');

                //TODO: Move the validation in attribute

                var imgExtension = inputModel.UploadImage.FileName.Substring(indexOfImgExtensionDot).ToLower();

                string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };

                if (!allowedExtensions.Contains(imgExtension))
                {
                    return RedirectToAction("ChangeProfilePicture", new MyProfileViewModel{IsValidProfilePicture = false});
                }

                myProfileServices.UploadProfilePictureCloudinary(this.User,inputModel);

            }

            return RedirectToAction("MyProfile");
        }
    }
}
