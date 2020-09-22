using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleSocial.Services.Models.Reports;
using SimpleSocial.Data.Common.Constants;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.FollowersDataServices;
using SimpleSocial.Services.DataServices.PostsServices;
using SimpleSocial.Services.DataServices.ReportsDataServices;
using System.Threading.Tasks;
using SimpleSocial.Services.DataServices.UsersDataServices;

namespace SimpleSocial.Web.Controllers
{
    public class ReportsController : BaseController
    {
        private readonly UserManager<SimpleSocialUser> userManager;
        private readonly IPostServices postServices;
        private readonly IReportsService reportsService;
        private readonly IFollowersServices followersServices;
        private IUserServices userServices;

        public ReportsController(
            UserManager<SimpleSocialUser> userManager,
            IPostServices postServices,
            IReportsService reportsService,
            IFollowersServices followersServices,
            IUserServices userServices)
        {
            this.userManager = userManager;
            this.postServices = postServices;
            this.reportsService = reportsService;
            this.followersServices = followersServices;
            this.userServices = userServices;
        }

        [Authorize]
        public IActionResult SubmitReport(int postId)
        {
            ReportViewModel viewModel = this.reportsService.GetSubmitReportViewModel(postId,User);
            if (viewModel == null)
            {
                var result = this.View("Error", this.ModelState);
                ViewData["Message"] = ErrorConstants.PageNotAvaivableMessage;
                result.StatusCode = (int)HttpStatusCode.BadRequest;
                return result;
            }
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Report(ReportViewModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                var result = this.View("Error", this.ModelState);
                result.StatusCode = (int)HttpStatusCode.BadRequest;
                return result;
            }

            var currentUserId = this.userServices.GetUserId(User);
            await reportsService.AddReport(currentUserId, inputModel.PostId, inputModel.ReportReason);
            return RedirectToAction("Index","NewsFeed");
        }

        [Authorize]
        public IActionResult ReportDetails(int id)
        {
            var viewModel = this.reportsService.GetReportDetails(id);
            if (viewModel == null)
            {
                var result = this.View("Error", this.ModelState);
                ViewData["Message"] = ErrorConstants.PageNotAvaivableMessage;
                result.StatusCode = (int)HttpStatusCode.NotFound;
                return result;
            }
            return View(viewModel);
        }

        [Authorize("Admin")]
        [HttpPost]
        public IActionResult DeleteReport(int id)
        {
            this.reportsService.DeleteReport(id, User);
            return RedirectToAction("SuccessfulAction", "Profiles", new { message = ControllerConstants.SuccessfullyDeletedReportMesssage });
        }
    }
}