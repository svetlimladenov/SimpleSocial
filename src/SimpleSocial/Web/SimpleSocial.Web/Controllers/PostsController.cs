using Microsoft.AspNetCore.Mvc;
using SimpleSocia.Services.Models.Account;
using SimpleSocia.Services.Models.Posts;
using SimpleSocial.Services.DataServices.PostsServices;

namespace SimpleSocial.Web.Controllers
{
    public class PostsController : BaseController
    {
        private readonly IPostServices postServices;

        public PostsController(IPostServices postServices)
        {
            this.postServices = postServices;
        }

        [HttpPost]
        public IActionResult Create(MyProfileViewModel viewModel)
        {
            var inputModel = viewModel.CreatePost;

            postServices.CreatePost(viewModel);

            return RedirectToAction("MyProfile", "Account");
        }

        public IActionResult PostDetails(string id)
        {
            var postViewModel = postServices.GetPostById(id);
           
            return View(postViewModel);
        }
    }
}
