using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleSocia.Services.Models.Followers;
using SimpleSocial.Services.DataServices.FollowersDataServices;
using X.PagedList;

namespace SimpleSocial.Web.Controllers
{
    public class FollowersController : BaseController
    {
        private readonly IFollowersServices followersServices;

        public FollowersController(IFollowersServices followersServices)
        {
            this.followersServices = followersServices;
        }

        [Authorize]
        public IActionResult WhoToFollow(int? pageNumbar)
        {
            var viewModel = new UsersListViewModel() {NoUsersWord = "more users left to follow."};
            var number = pageNumbar ?? 1;
            ViewBag.PageNum = number;
            var usersToFollow = followersServices.GetUsersToFollow(User);

            viewModel.Users = usersToFollow.ToPagedList(number, 12);
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult ChooseAction(string userId, string action)
        {
            if (action == "follow")
            {
                followersServices.Follow(userId, this.User);
            }
            else if (action == "unfollow")
            {
                followersServices.Unfollow(userId, this.User);
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