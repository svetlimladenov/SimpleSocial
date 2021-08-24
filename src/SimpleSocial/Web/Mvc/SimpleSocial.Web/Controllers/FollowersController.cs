using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleSocial.Services.Models.Followers;
using SimpleSocial.Services.DataServices.FollowersDataServices;
using X.PagedList;
using System.Threading.Tasks;
using System.Linq;
using SimpleSocial.Services.DataServices.UsersDataServices; 

namespace SimpleSocial.Web.Controllers
{
    public class FollowersController : BaseController
    {
        private readonly IFollowersServices followersServices;
        private readonly IUserServices userServices;

        public FollowersController(IFollowersServices followersServices, IUserServices userServices)
        {
            this.followersServices = followersServices;
            this.userServices = userServices;
        }

        [Authorize]
        public async Task<IActionResult> WhoToFollow(int? pageNumbar)
        {
            var viewModel = new UsersListViewModel() {NoUsersWord = "more users left to follow."};
            var number = pageNumbar ?? 1;
            ViewBag.PageNum = number;
            var usersToFollow = await followersServices.GetUsersToFollow<SimpleUserViewModel>(User);
            viewModel.Users = await usersToFollow.ToPagedListAsync(number, 12);
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult ChooseAction(int userId, string action)
        {
            var currentUserId = this.userServices.GetUserId(User);
            if (action == "follow")
            {
                followersServices.Follow(userId, currentUserId);
            }
            else if (action == "unfollow")
            {
                followersServices.Unfollow(userId, currentUserId);
            }
            else
            {
                return BadRequest();
            }

            return Ok();
        }
        
        [Authorize]
        public IActionResult Followers(int? pageNumbar)
        {
            var number = pageNumbar ?? 1;
            var followers = followersServices.GetFollowers(User);
            ViewBag.PageNum = number;    
            var viewModel = new UsersListViewModel {Users = followers.ToPagedList(number,12), NoUsersWord = "followers"};
            ViewData["FollowersCount"] = followers.Count;
            return View(viewModel);
        }

        [Authorize]
        public IActionResult Following(int? pageNumbar)
        {
            var number = pageNumbar ?? 1;
            var followings = followersServices.GetFollowings(User);
            ViewBag.PageNum = number;
            var viewModel = new UsersListViewModel{ Users = followings.ToPagedList(number,12) , NoUsersWord = "followings"};
            ViewData["FollowingsCount"] = followings.Count;

            return View(viewModel);
        }
    }
}