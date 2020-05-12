using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleSocial.Services.Models.Account;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Common.Constants;
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
        public async Task<IActionResult> PostComment(MyProfileViewModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                var result = this.View("Error", this.ModelState);
                ViewData["Message"] = ErrorConstants.SomethingWentWrongError;
                result.StatusCode = (int)HttpStatusCode.BadRequest;
                return result;
            }   

            var commentInputModel = inputModel.CommentInputModel;
            await this.commentsServices.CreateCommentAsync(commentInputModel);
            
            return RedirectToAction("PostDetails", "Posts", new { id = inputModel.CommentInputModel.PostId});
        }
    }
}