using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleSocia.Services.Models.Account;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices;
using SimpleSocial.Services.DataServices.Account;

namespace SimpleSocial.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IMyProfileServices myProfileServices;
        private readonly UserManager<SimpleSocialUser> userManager;
        private readonly IHostingEnvironment environment;
        private readonly IRepository<ProfilePicture> profilePicturesRepository;

        public AccountController(
            IMyProfileServices myProfileServices,
            UserManager<SimpleSocialUser> userManager,
            IHostingEnvironment environment,
            IRepository<ProfilePicture> profilePicturesRepository)
        {
            this.myProfileServices = myProfileServices;
            this.userManager = userManager;
            this.environment = environment;
            this.profilePicturesRepository = profilePicturesRepository;
        }


        public IActionResult MyProfile(MyProfileViewModel inputModel)
        {
            var viewModel = new MyProfileViewModel();

            viewModel.ProfilePicture = myProfileServices.GetProfilePicture(User);
            viewModel.Posts = myProfileServices.GetUserPosts(User);
            viewModel.WallId = myProfileServices.GetWallId(User);
            viewModel.UserId = userManager.GetUserId(User);
            viewModel.IsValidProfilePicture = inputModel.IsValidProfilePicture;


            return View(viewModel);
        }

        public IActionResult MyFriends()
        {
            return this.View();
        }

        public IActionResult UploadProfilePicture(UploadProfilePictureInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("MyProfile");
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
                    return RedirectToAction("MyProfile", new MyProfileViewModel{IsValidProfilePicture = false});
                }

                myProfileServices.UploadProfilePicture(this.User,inputModel, imgExtension);

            }

            return RedirectToAction("MyProfile");
        }
    }
}
