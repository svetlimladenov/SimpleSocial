using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;

namespace SimpleSocial.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IConfiguration configuration;

        public HomeController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("MyProfile","Account");
            }

            return LocalRedirect("/Identity/Account/Login");
        }   

        [Authorize]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }

        public IActionResult ConnectionString()
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var conString = this.configuration.GetConnectionString("DefaultConnection");
            var result = envName + Environment.NewLine + conString;
            return this.Json(result);
        }
    }
}
