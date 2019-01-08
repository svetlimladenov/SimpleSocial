using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleSocia.Services.Models.Account;
using SimpleSocia.Services.Models.Posts;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.PostsServices;
using SimpleSocial.Services.DataServices.UsersDataServices;

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
        public IActionResult Create(MyProfileViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                postServices.CreatePost(viewModel);
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
        public IActionResult PostDetails(string id)
        {
            var currentUserId = this.userManager.GetUserId(User);
            var viewModel = postServices.GetSinglePostViewComponentModel(id, currentUserId);
            if (viewModel == null)
            {
                var result = this.View("Error", this.ModelState);
                ViewData["Message"] = "This page is not avaivable";
                result.StatusCode = (int)HttpStatusCode.BadRequest;
                return result;
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult DeletePost(string id)
        {
            postServices.DeletePost(id, User);
            return RedirectToAction("SuccessfullAction", "Profiles", new { message = "You have successfully deleted this post." });
        }

        public IActionResult GetPosts(int pageNumber)
        {
            var currentUserId = userManager.GetUserId(User);
            var posts = postServices.GetNewsFeedPosts(currentUserId, pageNumber);
            var viewModel = new PostsFeedAndUserInfoViewModel()
            {
                Posts = posts,
                CurrentUserInfo = userServices.GetUserInfo(currentUserId, currentUserId),
                UserProfileInfo = userServices.GetUserInfo(currentUserId, currentUserId),
            };
            var partial = this.PartialView("Components/ListOfPosts/Default", viewModel);
            return partial;
        }
    }
}
