using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        public IActionResult Index()
        {
            ViewBag.CountryList = profileDetailsServices.GetCounties();
            var currentUser = this.userServices.GetUser(User);
            var viewModel = Mapper.Map<ProfileDetailsInputModel>(currentUser);
            return this.View(viewModel);
        }

        [Authorize]
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

        [Authorize]
        [HttpPost]
        public IActionResult LivingPlace(ProfileDetailsInputModel inputModel)
        {
            profileDetailsServices.SetLivingPlace(inputModel,User);
            return RedirectToAction("SuccessfullInput");
        }

        [Authorize]
        [HttpPost]
        public IActionResult BirthDay(ProfileDetailsInputModel inputModel)
        {
            profileDetailsServices.SetBirthDay(inputModel, User);
            return RedirectToAction("SuccessfullInput");
        }

        [Authorize]
        public IActionResult SuccessfullInput()
        {
            return View();
        }
    }
}
