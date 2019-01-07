using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleSocia.Services.Models.Reports;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.FollowersDataServices;
using SimpleSocial.Services.DataServices.PostsServices;
using SimpleSocial.Services.DataServices.ReportsDataServices;

namespace SimpleSocial.Web.Controllers
{
    public class ReportsController : BaseController
    {
        private readonly UserManager<SimpleSocialUser> userManager;
        private readonly IPostServices postServices;
        private readonly IReportsService reportsService;
        private readonly IFollowersServices followersServices;

        public ReportsController(
            UserManager<SimpleSocialUser> userManager,
            IPostServices postServices,
            IReportsService reportsService,
            IFollowersServices followersServices)
        {
            this.userManager = userManager;
            this.postServices = postServices;
            this.reportsService = reportsService;
            this.followersServices = followersServices;
        }

        [Authorize]
        public IActionResult SubmitReport(string postId)
        {
            var postAuthor = postServices.GetPostAuthor(postId);
            var genderText = "Him";
            var currentUserId = userManager.GetUserId(User);
            if (postAuthor.Gender == Gender.Male)
            {
                genderText = "Him";
            }
            else if(postAuthor.Gender == Gender.Famale)
            {
                genderText = "Her";
            }
            var isBeingFollowedByCurrentUser = this.followersServices.IsBeingFollowedBy(postAuthor.Id, currentUserId) || postAuthor.Id == currentUserId;
            var viewModel = new ReportViewModel
            {
                PostId = postId,
                ReportReason = new ReportReason(),
                PostAuthorName = postAuthor.UserName,
                PostAuthorId = postAuthor.Id,
                GenderText = genderText,
                IsBeingFollowedByCurrentUser = isBeingFollowedByCurrentUser
            };

            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Report(ReportViewModel inputModel)
        {
            var currentUserId = this.userManager.GetUserId(User);
            reportsService.AddReport(currentUserId,inputModel.PostId,inputModel.ReportReason);
            return RedirectToAction("Index","NewsFeed");
        }

        [Authorize]
        public IActionResult ReportDetails(string id)
        {
            var viewModel = this.reportsService.GetReportDetails(id);
            return View(viewModel);
        }
    }
}