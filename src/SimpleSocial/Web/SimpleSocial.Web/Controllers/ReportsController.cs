using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleSocia.Services.Models.Reports;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.PostsServices;
using SimpleSocial.Services.DataServices.ReportsDataServices;

namespace SimpleSocial.Web.Controllers
{
    public class ReportsController : BaseController
    {
        private readonly UserManager<SimpleSocialUser> userManager;
        private readonly IPostServices postServices;
        private readonly IReportsService reportsService;

        public ReportsController(
            UserManager<SimpleSocialUser> userManager,
            IPostServices postServices,
            IReportsService reportsService)
        {
            this.userManager = userManager;
            this.postServices = postServices;
            this.reportsService = reportsService;
        }

        [Authorize]
        public IActionResult SubmitReport(string postId)
        {
            var postAuthor = postServices.GetPostAuthor(postId);
            var genderText = "Him";
            if (postAuthor.Gender == Gender.Male)
            {
                genderText = "Him";
            }
            else if(postAuthor.Gender == Gender.Famale)
            {
                genderText = "Her";
            }
            var viewModel = new ReportsViewModel
            {
                PostId = postId,
                ReportReason = new ReportReason(),
                PostAuthorName = postAuthor.UserName,
                PostAuthorId = postAuthor.Id,
                GenderText = genderText,
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Report(ReportsViewModel inputModel)
        {
            var currentUserId = this.userManager.GetUserId(User);
            reportsService.AddReport(currentUserId,inputModel.PostId,inputModel.ReportReason);
            return RedirectToAction("Index","NewsFeed");
        }
    }
}