using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleSocia.Services.Models.Account;
using SimpleSocia.Services.Models.Posts;

namespace SimpleSocial.Web.Components
{
    [ViewComponent(Name = "ListOfPosts")]
    public class ListOfPostsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(ICollection<PostViewModel> posts)
        {
            var viewModel = new MyProfileViewModel();
            viewModel.Posts = posts;
            return View(viewModel);
        }
    }
}
