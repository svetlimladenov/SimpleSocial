using Microsoft.AspNetCore.Mvc;
using SimpleSocia.Services.Models.Account;
using SimpleSocial.Services.DataServices.PostsServices;

namespace SimpleSocial.Web.Controllers
{
    public class PostsController : BaseController
    {
        private readonly IPostServices createPostServices;

        public PostsController(IPostServices createPostServices)
        {
            this.createPostServices = createPostServices;
        }

        [HttpPost]
        public IActionResult Create(MyProfileViewModel viewModel)
        {
            var inputModel = viewModel.CreatePost;

            createPostServices.CreatePostAsync(viewModel);

            return RedirectToAction("MyProfile", "Account");
        }
    }
}
