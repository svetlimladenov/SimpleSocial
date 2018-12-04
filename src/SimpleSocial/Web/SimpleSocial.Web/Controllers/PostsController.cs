using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleSocia.Services.Models.Account;
using SimpleSocial.Services.DataServices;
using SimpleSocial.Web.Models;

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

            createPostServices.CreatePost(viewModel);

            return RedirectToAction("MyProfile", "Account");
        }
    }
}
