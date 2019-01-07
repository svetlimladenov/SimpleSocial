using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleSocia.Services.Models.Account;
using SimpleSocial.Services.DataServices.CommentsServices;

namespace SimpleSocial.Web.Controllers
{
    public class CommentsController : BaseController
    {
        private readonly ICommentsServices commentsServices;

        public CommentsController(ICommentsServices commentsServices)
        {
            this.commentsServices = commentsServices;
        }

        [Authorize]
        [HttpPost]
        public IActionResult PostComment(MyProfileViewModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                var result = this.View("Error", this.ModelState);
                ViewData["Message"] = "Oops something went wrong.";
                result.StatusCode = (int)HttpStatusCode.BadRequest;
                return result;
            }

            var commentInputModel = inputModel.CommentInputModel;
            this.commentsServices.CreateComment(commentInputModel);
            
            return RedirectToAction("PostDetails", "Posts", new { id = inputModel.CommentInputModel.PostId});
        }
    }
}