using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleSocial.Models;
using SimpleSocial.Services.PostsServices;
using SimpleSocial.ViewModels.Account;

namespace SimpleSocial.Controllers
{
    public class PostsController : BaseController
    {
        private readonly ICreatePostServices createPostServices;

        public PostsController(ICreatePostServices createPostServices)
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