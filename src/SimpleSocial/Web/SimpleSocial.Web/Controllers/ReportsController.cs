using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            ReportViewModel viewModel = this.reportsService.GetSubmitReportViewModel(postId,User);
            if (viewModel == null)
            {
                var result = this.View("Error", this.ModelState);
                ViewData["Message"] = "This page is not avaivable";
                result.StatusCode = (int)HttpStatusCode.BadRequest;
                return result;
            }
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Report(ReportViewModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                var result = this.View("Error", this.ModelState);
                result.StatusCode = (int)HttpStatusCode.BadRequest;
                return result;
            }

            var currentUserId = this.userManager.GetUserId(User);
            reportsService.AddReport(currentUserId,inputModel.PostId,inputModel.ReportReason);
            return RedirectToAction("Index","NewsFeed");
        }

        [Authorize]
        public IActionResult ReportDetails(string id)
        {
            var viewModel = this.reportsService.GetReportDetails(id);
            if (viewModel == null)
            {
                var result = this.View("Error", this.ModelState);
                ViewData["Message"] = "This page is not avaivable";
                result.StatusCode = (int)HttpStatusCode.NotFound;
                return result;
            }
            return View(viewModel);
        }

        [Authorize("Admin")]
        [HttpPost]
        public IActionResult DeleteReport(string id)
        {
            this.reportsService.DeleteReport(id,User);
            return RedirectToAction("SuccessfullAction", "Profiles", new { message = "You have successfully deleted this report." });
        }
    }
}