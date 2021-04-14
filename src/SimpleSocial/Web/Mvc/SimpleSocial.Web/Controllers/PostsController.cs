using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleSocial.Services.Models.Account;
using SimpleSocial.Data.Common.Constants;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.PostsServices;
using SimpleSocial.Services.DataServices.UsersDataServices;
using System.Threading.Tasks;

namespace SimpleSocial.Web.Controllers
{
    public class PostsController : BaseController
    {
        private readonly IPostServices postServices;
        private readonly UserManager<SimpleSocialUser> userManager;
        private readonly IUserServices userServices;

        public PostsController(
            IPostServices postServices,
            UserManager<SimpleSocialUser> userManager,
            IUserServices userServices)
        {
            this.postServices = postServices;
            this.userManager = userManager;
            this.userServices = userServices;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(MyProfileViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await postServices.CreatePost(viewModel);
                return RedirectToAction("MyProfile", "Account");
            }
            else
            {
                var result = this.View("Error", this.ModelState);
                result.StatusCode = (int)HttpStatusCode.BadRequest;
                return result;
            }

        }

        [Authorize]
        public IActionResult PostDetails(int id)
        {
            var currentUserId = this.userServices.GetUserId(User);
            var viewModel = postServices.GetSinglePostViewComponentModel(id, currentUserId);
            if (viewModel == null)
            {
                var result = this.View("Error", this.ModelState);
                ViewData["Message"] = ErrorConstants.PageNotAvaivableMessage;
                result.StatusCode = (int)HttpStatusCode.BadRequest;
                return result;
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult DeletePost(int id)
        {
            postServices.DeletePost(id, User);
            return RedirectToAction("SuccessfulAction", "Profiles", new { message = ControllerConstants.SuccessfullyDeletedPostMessage });
        }

        public async Task<IActionResult> GetPosts(int pageNumber)
        {
            var currentUserId = this.userServices.GetUserId(User);
            var posts = postServices.GetNewsFeedPosts(currentUserId, pageNumber);
            var viewModel = new PostsFeedAndUserInfoViewModel()
            {
                Posts = posts,
                CurrentUserInfo = await userServices.GetUserInfo(currentUserId, currentUserId),
                UserProfileInfo = await userServices.GetUserInfo(currentUserId, currentUserId),
            };
            var partial = this.PartialView("Components/ListOfPosts/Default", viewModel);
            return partial;
        }
    }
}
