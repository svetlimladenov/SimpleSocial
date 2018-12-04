using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;
using SimpleSocial.Web.Models;

namespace SimpleSocial.Web.Controllers
{
    public class HomeController : Controller
    {
        //write service and only there use reposiro
        private readonly IRepository<SimpleSocialUser> postRepository;

        public HomeController(IRepository<SimpleSocialUser> postRepository)
        {
            this.postRepository = postRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = $"My app has {postRepository.All().Count()} users.    ";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
