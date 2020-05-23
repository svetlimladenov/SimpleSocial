using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.LikesDataServices;
using System.Threading.Tasks;

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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GetAction(string isLiked, string postId)
        {
            var userId = this.userManager.GetUserId(User);
            if (isLiked.ToLower() == "false")
            {
                await likesServices.Like(postId, userId);
            }
            else if(isLiked.ToLower() == "true")
            {
                await likesServices.UnLike(postId, userId);
            }
            else
            {
                return NotFound();
            }
            
            return Ok();
        }
    }
}