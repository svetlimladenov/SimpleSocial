using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleSocia.Services.Models.Account;
using SimpleSocia.Services.Models.Followers;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Common.Constants;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.Account;
using SimpleSocial.Services.DataServices.FollowersDataServices;
using SimpleSocial.Services.DataServices.PostsServices;
using SimpleSocial.Services.DataServices.UsersDataServices;

namespace SimpleSocial.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IMyProfileServices myProfileServices;
        private readonly IUserServices userServices;
        private readonly IPostServices postServices;
        private readonly IFollowersServices followersServices;
        private readonly UserManager<SimpleSocialUser> userManager;

        public AccountController(
            IMyProfileServices myProfileServices,
            IUserServices userServices,
            IPostServices postServices,
            IFollowersServices followersServices,
            UserManager<SimpleSocialUser> userManager
            )
        {
            this.myProfileServices = myProfileServices;
            this.userServices = userServices;
            this.postServices = postServices;
            this.followersServices = followersServices;
            this.userManager = userManager;

        }

        [Authorize]
        public IActionResult MyProfile(MyProfileViewModel inputModel)
        {
            var userId = this.userManager.GetUserId(User);
            var whoToFollowList = new UsersListViewModel()
            {
                Users = followersServices.GetUsersToFollow(User).ToList(),
                UsersCount = ControllerConstants.WhoToFollowPartialFollowerCount,
            };
            var viewModel = new MyProfileViewModel
            {
                CurrentUserInfo = userServices.GetUserInfo(userId, userId),
                Posts = postServices.GetUserPosts(userId, userId,0),
                IsValidProfilePicture = inputModel.IsValidProfilePicture,
                UserProfileInfo = userServices.GetUserInfo(userId, userId),
                WhoToFollow = whoToFollowList
            };

            return View(viewModel);
        }

        public IActionResult GetMyPosts(int pageNumber)
        {
            var currentUserId = userManager.GetUserId(User);
            var posts = postServices.GetUserPosts(currentUserId,currentUserId, pageNumber);
            var viewModel = new PostsFeedAndUserInfoViewModel()
            {
                Posts = posts,
                CurrentUserInfo = userServices.GetUserInfo(currentUserId, currentUserId),
                UserProfileInfo = userServices.GetUserInfo(currentUserId, currentUserId),
            };
            var partial = this.PartialView("Components/ListOfPosts/Default", viewModel);
            return partial;
        }

        [Authorize]
        public IActionResult ChangeProfilePicture()
        {
            return this.View();
        }

        [Authorize]
        public IActionResult UploadProfilePicture(UploadProfilePictureInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ChangeProfilePicture");
            }

            if (inputModel.UploadImage != null)
            {
                var indexOfImgExtensionDot = inputModel.UploadImage.FileName.IndexOf('.');

                var imgExtension = inputModel.UploadImage.FileName.Substring(indexOfImgExtensionDot).ToLower();

                string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };

                if (!allowedExtensions.Contains(imgExtension))
                {
                    return RedirectToAction("ChangeProfilePicture", new MyProfileViewModel { IsValidProfilePicture = false });
                }
                myProfileServices.UploadProfilePictureCloudinary(this.User, inputModel);
            }

            return RedirectToAction("MyProfile");
        }
    }
}
