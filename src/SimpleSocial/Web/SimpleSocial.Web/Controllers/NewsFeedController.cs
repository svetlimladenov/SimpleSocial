using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleSocial.Services.Models.Account;
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

        [Authorize]
        public IActionResult Index()
        {
            var currentUserId = userManager.GetUserId(User);
            var posts = postServices.GetNewsFeedPosts(currentUserId,0);
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