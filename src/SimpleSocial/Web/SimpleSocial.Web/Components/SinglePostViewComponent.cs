using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleSocial.Services.Models.Comments;
using SimpleSocial.Services.Models.Posts;

namespace SimpleSocial.Web.Components
{
    [ViewComponent(Name = "SinglePost")]
    public class SinglePostViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(
            string currentUserId,
            PostViewModel post,
            string profilePictureURL)
        {
            SinglePostViewComponentModel viewModel = new SinglePostViewComponentModel
            {
                Post = post,
                CommentInputModel = new CommentInputModel(),
                ProfilePictureURL = profilePictureURL,
                PostVisitorId = currentUserId,
                PostAuthorId = post.UserId,
            };
            return View(viewModel);
        }

    }
}
