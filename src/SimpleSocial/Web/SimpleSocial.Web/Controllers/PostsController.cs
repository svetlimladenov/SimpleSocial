using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleSocia.Services.Models.Account;
using SimpleSocia.Services.Models.Posts;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.PostsServices;

namespace SimpleSocial.Web.Controllers
{
    public class PostsController : BaseController
    {
        private readonly IPostServices postServices;
        private readonly UserManager<SimpleSocialUser> userManager;

        public PostsController(
            IPostServices postServices,
            UserManager<SimpleSocialUser> userManager)
        {
            this.postServices = postServices;
            this.userManager = userManager;
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
            postServices.DeletePost(id);
            return RedirectToAction("SuccessfullInput", "Profiles", new { message = "You have successfully deleted this post." });
        }
    }
}
