using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SimpleSocia.Services.Models.Account;
using SimpleSocia.Services.Models.SignUp;
using SimpleSocial.Data.Common;
using SimpleSocial.Services.DataServices.Account;
using SimpleSocial.Services.DataServices.SignUpDetails;

namespace SimpleSocial.Web.Controllers
{
    public class ProfileDetailsController : BaseController
    {
        private readonly IProfileDetailsServices profileDetailsServices;
        private readonly IUserServices userServices;

        public ProfileDetailsController(
            IProfileDetailsServices profileDetailsServices,
            IUserServices userServices)
        {
            this.profileDetailsServices = profileDetailsServices;
            this.userServices = userServices;
        }

        public IActionResult Index()
        {
            var currentUser = this.userServices.GetUser(User);
            var viewModel = Mapper.Map<ProfileDetailsInputModel>(currentUser);
            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult NamesInput(ProfileDetailsInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            profileDetailsServices.SetNames(inputModel,this.User);
            return RedirectToAction("SuccessfullInput");
        }

        [HttpPost]
        public IActionResult LivingPlace(ProfileDetailsInputModel inputModel)
        {
            return RedirectToAction("SuccessfullInput");
        }

        [HttpPost]
        public IActionResult BirthDay(ProfileDetailsInputModel inputModel)
        {
            profileDetailsServices.SetBirthDay(inputModel, User);
            return RedirectToAction("SuccessfullInput");
        }

        public IActionResult SuccessfullInput()
        {
            return View();
        }
    }
}
