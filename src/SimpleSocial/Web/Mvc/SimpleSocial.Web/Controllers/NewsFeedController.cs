using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleSocial.Services.Models.Account;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.PostsServices;
using SimpleSocial.Services.DataServices.UsersDataServices;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Index()
        {
            var currentUserId = this.userServices.GetUserId(User);
            var posts = postServices.GetNewsFeedPosts(currentUserId,0);
            var viewModel = new PostsFeedAndUserInfoViewModel()
            {
                Posts = posts,
                CurrentUserInfo = await userServices.GetUserInfo(currentUserId, currentUserId),       
                UserProfileInfo = await userServices.GetUserInfo(currentUserId, currentUserId),
            };
            return View(viewModel);
        }
    }
}