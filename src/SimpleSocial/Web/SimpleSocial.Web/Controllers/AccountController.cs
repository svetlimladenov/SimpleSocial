using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleSocia.Services.Models.Account;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.Account;
using SimpleSocial.Services.DataServices.PostsServices;

namespace SimpleSocial.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IMyProfileServices myProfileServices;
        private readonly IPostServices postServices;
        private readonly UserManager<SimpleSocialUser> userManager;
        private readonly IHostingEnvironment environment;
        private readonly IRepository<ProfilePicture> profilePicturesRepository;

        public AccountController(
            IMyProfileServices myProfileServices,
            IPostServices postServices,
            UserManager<SimpleSocialUser> userManager,
            IHostingEnvironment environment,
            IRepository<ProfilePicture> profilePicturesRepository)
        {
            this.myProfileServices = myProfileServices;
            this.postServices = postServices;
            this.userManager = userManager;
            this.environment = environment;
            this.profilePicturesRepository = profilePicturesRepository;
        }


        public IActionResult MyProfile(MyProfileViewModel inputModel)
        {
            var viewModel = new MyProfileViewModel
            {
                ProfilePicture = myProfileServices.GetProfilePicture(User),
                Posts = postServices.GetUserPosts(User),
                WallId = myProfileServices.GetWallId(User),
                UserId = userManager.GetUserId(User),
                IsValidProfilePicture = inputModel.IsValidProfilePicture
            };

            return View(viewModel);
        }

        public IActionResult MyFriends()
        {
            return this.View();
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
                var userId = this.userManager.GetUserId(User);
                var indexOfImgExtensionDot = inputModel.UploadImage.FileName.IndexOf('.');

                //TODO: Move the validation in attribute

                var imgExtension = inputModel.UploadImage.FileName.Substring(indexOfImgExtensionDot).ToLower();

                string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };

                if (!allowedExtensions.Contains(imgExtension))
                {
                    return RedirectToAction("ChangeProfilePicture", new MyProfileViewModel{IsValidProfilePicture = false});
                }

                myProfileServices.UploadProfilePicture(this.User,inputModel, imgExtension);

            }

            return RedirectToAction("MyProfile");
        }
    }
}
