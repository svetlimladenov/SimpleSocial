using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.LikesDataServices;

namespace SimpleSocial.Web.Controllers
{
    public class LikesController : BaseController
    {
        private readonly ILikesServices likesServices;
        private readonly UserManager<SimpleSocialUser> userManager;

        public LikesController(
            ILikesServices likesServices,
            UserManager<SimpleSocialUser> userManager)
        {
            this.likesServices = likesServices;
            this.userManager = userManager;
        }

        [HttpPost]
        public IActionResult GetAction(string isLiked, string postId)
        {
            //TODO: Save like to db;
            //TODO: Success in ajax;
            var userId = userManager.GetUserId(this.User);

            if (isLiked.ToLower() == "false")
            {
                likesServices.Like(postId, userId);
            }
            else if(isLiked.ToLower() == "true")
            {
                likesServices.UnLike(postId, userId);
            }
            else
            {
                return NotFound();
            }
            
            return Ok();
        }
    }
}