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
            var inputModel = viewModel.CreatePost;

            postServices.CreatePost(viewModel);

            return RedirectToAction("MyProfile", "Account");
        }

        [Authorize]
        public IActionResult PostDetails(string id, string userId)
        {
            //TODO : CHANGE LOGIC HERE
            //if (!postServices.UserCanSeePost(id,User))
            //{
            //    return BadRequest();
            //}
            
            var viewModel = postServices.GetSinglePostViewComponentModel(id, userId);
 
            return View(viewModel);
        }
    }
}
