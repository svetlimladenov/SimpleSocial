using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleSocia.Services.Models.Comments;
using SimpleSocia.Services.Models.Posts;
using SimpleSocial.Data.Models;

namespace SimpleSocial.Web.Components
{
    [ViewComponent(Name = "SinglePost")]
    public class SinglePostViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(
            PostViewModel post,
            CommentInputModel commentInputModel,
            ProfilePicture profilePicture,
            string likeClassName)
        {
            SinglePostViewComponentModel viewModel = new SinglePostViewComponentModel();
            viewModel.Post = post;
            viewModel.CommentInputModel = commentInputModel;
            viewModel.ProfilePicture = profilePicture;
            viewModel.LikeClassName = likeClassName;
            return View(viewModel);
        }

    }
}
