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

        [HttpPost]
        public IActionResult PostComment(MyProfileViewModel inputModel, string userId)
        {
            var commentInputModel = inputModel.CommentInputModel;
            this.commentsServices.CreateComment(commentInputModel);
            
            return RedirectToAction("Index", "Profiles", new { userId = userId});
        }
    }
}