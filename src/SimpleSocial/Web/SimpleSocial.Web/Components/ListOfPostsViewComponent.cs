using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleSocia.Services.Models.Account;

namespace SimpleSocial.Web.Components
{
    [ViewComponent(Name = "ListOfPosts")]
    public class ListOfPostsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(PostsFeedAndUserInfoViewModel posts)
        {
            return View(posts);
        }
    }
}
