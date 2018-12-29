using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleSocial.Data.Models;

namespace SimpleSocial.Web.Controllers
{
    public class ProfilesController : BaseController
    {
        private readonly UserManager<SimpleSocialUser> userManager;

        public ProfilesController(UserManager<SimpleSocialUser> userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult Index(string userId)
        {
            if (this.userManager.GetUserId(User) == userId)
            {
                return RedirectToAction("MyProfile", "Account");
            }
            

            return this.View();
        }

        [Authorize]
        public IActionResult SuccessfullInput()
        {
            return View();
        }
    }
}