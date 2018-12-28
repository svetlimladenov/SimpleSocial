using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleSocia.Services.Models.Followers;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.FollowersDataServices;
using SimpleSocial.Services.Mapping;

namespace SimpleSocial.Web.Controllers
{
    public class FollowersController : BaseController
    {
        private readonly IFollowersServices followersServices;

        public FollowersController(IFollowersServices followersServices)
        {
            this.followersServices = followersServices;
        }

        public IActionResult WhoToFollow()
        {
            var viewModel = new UsersListViewModel();
            viewModel.Users = followersServices.GetUsersToFollow(User);
            return View(viewModel);
        }

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

        public IActionResult Followers()
        {
            var viewModel = new UsersListViewModel {Users = followersServices.GetFollowers(User)};
            return View(viewModel);
        }

        public IActionResult Following()
        {
            var viewModel = new UsersListViewModel{ Users = followersServices.GetFollowings(User) };
            return View(viewModel);
        }
    }
}