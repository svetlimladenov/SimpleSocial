using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleSocia.Services.Models.Account;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.PostsServices;
using SimpleSocial.Services.DataServices.UsersDataServices;

namespace SimpleSocial.Web.Controllers
{
    public class NewsFeedController : Controller
    {
        private readonly UserManager<SimpleSocialUser> userManager;
        private readonly IPostServices postServices;
        private readonly IUserServices userServices;

        public NewsFeedController(
            UserManager<SimpleSocialUser> userManager,
            IPostServices postServices,
            IUserServices userServices)
        {
            this.userManager = userManager;
            this.postServices = postServices;
            this.userServices = userServices;
        }

        public IActionResult Index()
        {
            var currentUserId = userManager.GetUserId(User);
            var posts = postServices.GetNewsFeedPosts(currentUserId);
            var viewModel = new PostsFeedAndUserInfoViewModel()
            {
                Posts = posts,
                CurrentUserInfo = userServices.GetUserInfo(currentUserId,currentUserId),       
                UserProfileInfo = userServices.GetUserInfo(currentUserId,currentUserId),
            };
            return View(viewModel);
        }
    }
}