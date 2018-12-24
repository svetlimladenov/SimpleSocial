using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SimpleSocial.Web.Controllers
{
    public class LikesController : BaseController
    {
        [HttpPost]
        public IActionResult GetAction(string action, string postId)
        {
            //TODO: Save like to db;
            //TODO: Success in ajax;

            if (action == "true")
            {
                return RedirectToAction("Unlike", postId);
            }
            
            return RedirectToAction("Like", postId);
        }

        public void Like(string postId)
        {

        }

        public void UnLike(string postId)
        {

        }
    }
}