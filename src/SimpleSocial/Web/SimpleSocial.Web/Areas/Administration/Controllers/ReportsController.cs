using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SimpleSocial.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize("Admin")]
    public class ReportsController : Controller
    {
        public IActionResult All()
        {
            return View();
        }
    }
}