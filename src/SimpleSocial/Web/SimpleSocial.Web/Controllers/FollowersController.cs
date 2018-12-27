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
            var viewModel = new FollowersViewModel();
            viewModel.Users = followersServices.GetUsersToFollow(User);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Follow(string userToFollowId)
        {
            followersServices.Follow(userToFollowId,this.User);
            return Ok();
        }

        public IActionResult Followers()
        {
            return View();
        }

        public IActionResult Following()
        {
            return View();
        }
    }
}