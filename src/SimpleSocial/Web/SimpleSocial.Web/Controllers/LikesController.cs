using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.LikesDataServices;
using SimpleSocial.Services.DataServices.UsersDataServices;
using System.Threading.Tasks;

namespace SimpleSocial.Web.Controllers
{
    public class LikesController : BaseController
    {
        private readonly ILikesServices likesServices;
        private readonly IUserServices userServices;

        public LikesController(
            ILikesServices likesServices,
            IUserServices userServices)
        {
            this.likesServices = likesServices;
            this.userServices = userServices;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> GetAction(string isLiked, int postId)
        {
            var userId = this.userServices.GetUserId(User);
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