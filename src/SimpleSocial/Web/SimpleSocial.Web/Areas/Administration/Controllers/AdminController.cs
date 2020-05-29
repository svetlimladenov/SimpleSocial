using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleSocial.Services.DataServices.ReportsDataServices;
using SimpleSocial.Services.DataServices.SearchDataServices;
using SimpleSocial.Services.DataServices.UsersDataServices;
using SimpleSocial.Web.Areas.Administration.Services;
using SimpleSocial.Web.Areas.Administration.ViewModels;
using X.PagedList;

namespace SimpleSocial.Web.Areas.Administration.Controllers
{
    [Authorize("Admin")]
    [Area("Administration")]
    public class AdminController : Controller
    {
        private readonly IAdministrationServices administrationServices;
        private readonly ISearchServices searchServices;
        private readonly IUserServices userServices;
        private readonly IReportsService reportsService;

        public AdminController(
            IAdministrationServices administrationServices,
            ISearchServices searchServices,
            IUserServices userServices,
            IReportsService reportsService)
        {
            this.administrationServices = administrationServices;
            this.searchServices = searchServices;
            this.userServices = userServices;
            this.reportsService = reportsService;
        }

        [Authorize("Admin")]
        public IActionResult Index()
        {
            ViewData["Quote"] = administrationServices.GetRandomQuote();
            return View();
        }

        [HttpGet("Administration/Admin/AllReports/{pageNumber:int}")]
        [Authorize("Admin")]
        public async Task<IActionResult> AllReports(int? pageNumber = 1)
        {
            // TODO: Add paging in the view. move 10 to constant
            var repostsCount = await this.reportsService.GetReportsCount();
            var pagesCount = (int)Math.Ceiling(repostsCount / 10d);
            var viewModel = new AllReportsViewModel
            {
                PostReports = await this.administrationServices.GetAllReports((int)pageNumber - 1, 10),
                PagesCount = pagesCount,
                CurrentPageNumber = (int)pageNumber
            };

            ViewBag.PageNum = pageNumber;
            return View(viewModel);
        }

        [Authorize("Admin")]
        public IActionResult ManageUsers(string searchText)
        {
            if (!string.IsNullOrEmpty(searchText))
            {
                var result = this.searchServices.GetResultOfSearch(searchText, User);
                var users = result.UsersFound.Users.ToList();
                var usersFoundToPromoteDemote = new PromoteDemoteViewModel()
                {
                    AdminUsers = administrationServices.GetAdminUsers(User, users),
                    NonAdminUsers = administrationServices.GetNonAdminUsers(User, users),
                    AllUsers = this.userServices.GetAllUsernames()
                };

                return View(usersFoundToPromoteDemote);
            }

            var usersToPromoteDemote = new PromoteDemoteViewModel
            {
                AdminUsers = administrationServices.GetAdminUsers(User),
                NonAdminUsers = administrationServices.GetNonAdminUsers(User),
                AllUsers = this.userServices.GetAllUsernames()
            };

            return View(usersToPromoteDemote);
        }

        [Authorize("Admin")]
        public async Task<IActionResult> Promote(string id)
        {
            await administrationServices.PromoteUser(id);
            return RedirectToAction("ManageUsers");
        }

        [Authorize("Admin")]
        public async Task<IActionResult> Demote(string id)
        {
            await administrationServices.DemoteUser(id);
            return RedirectToAction("ManageUsers");
        }
    }
}