using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleSocial.Services.Models.Account;
using SimpleSocial.Services.Models.Followers;
using SimpleSocial.Data.Common.Constants;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.FollowersDataServices;
using SimpleSocial.Services.DataServices.PostsServices;
using SimpleSocial.Services.DataServices.ProfilePictureServices;
using SimpleSocial.Services.DataServices.UsersDataServices;
using System.Threading.Tasks;
using SimpleSocial.Services.Models.Users;
using SimpleSocial.Application.Users.Queries;
using MediatR;

namespace SimpleSocial.Web.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IMediator mediator; 
        private readonly IUserServices userServices;
        private readonly IPostServices postServices;
        private readonly IFollowersServices followersServices;
        private readonly IProfilePictureService profilePictureService;
        private readonly UserManager<SimpleSocialUser> userManager;

        public AccountController(
            IUserServices userServices,
            IPostServices postServices,
            IFollowersServices followersServices,
            IProfilePictureService profilePictureService,
            UserManager<SimpleSocialUser> userManager,
            IMediator mediator)
        {
            this.userServices = userServices;
            this.postServices = postServices;
            this.followersServices = followersServices;
            this.profilePictureService = profilePictureService;
            this.userManager = userManager;
            this.mediator = mediator;
        }

        [Authorize]
        public async Task<IActionResult> MyProfile(MyProfileViewModel inputModel)
        {
            var userId = this.userServices.GetUserId(User); // find out a better way to get this id store is somewhere in the browser 
            var whoToFollowList = new UsersListViewModel()
            {
                Users = await followersServices.GetUsersToFollow(User),
                UsersCount = ControllerConstants.WhoToFollowPartialFollowerCount,
            };

            //TODO: Fix the whole logic behind this method because - dont user GetUserInfo
            var viewModel = new MyProfileViewModel
            {
                CurrentUserInfo = await userServices.GetUserInfo(userId, userId),
                Posts = postServices.GetUserPosts(userId, userId, 0),
                IsValidProfilePicture = inputModel.IsValidProfilePicture,
                UserProfileInfo = await userServices.GetUserInfo(userId, userId),
                WhoToFollow = whoToFollowList
            };

            this.ViewData["UserId"] = userId;
            return View(viewModel);
        }

        public async Task<IActionResult> GetMyPosts(int pageNumber)
        {
            var currentUserId = this.userServices.GetUserId(User);
            var posts = postServices.GetUserPosts(currentUserId,currentUserId, pageNumber);

            var viewModel = new PostsFeedAndUserInfoViewModel()
            {
                Posts = posts,
                CurrentUserInfo = await userServices.GetUserInfo(currentUserId, currentUserId),
                UserProfileInfo = await userServices.GetUserInfo(currentUserId, currentUserId),
            };

            return this.PartialView("Components/ListOfPosts/Default", viewModel);
        }

        [Authorize]
        public IActionResult ChangeProfilePicture()
        {
            return this.View();
        }

        [Authorize]
        public async Task<IActionResult> UploadProfilePicture(UploadProfilePictureInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ChangeProfilePicture");
            }

            if (inputModel.UploadImage != null)
            {
                var isValid = this.profilePictureService.VerifyPicture(inputModel);

                if (!isValid)
                {
                    return RedirectToAction("ChangeProfilePicture", new MyProfileViewModel { IsValidProfilePicture = false });
                }

                await profilePictureService.UploadProfilePictureCloudinary(this.User, inputModel);
            }

            return RedirectToAction("MyProfile");
        }

        [HttpGet]
        [Route("Account/GetUserBoxInfo/{id?}")]
        public async Task<IActionResult> GetUserBoxInfo(int id)
        {
            var query = new GetUserInfoQuery() { UserId = id };
            var response = await this.mediator.Send(query);
            return Json(response);
        }
    }
}
