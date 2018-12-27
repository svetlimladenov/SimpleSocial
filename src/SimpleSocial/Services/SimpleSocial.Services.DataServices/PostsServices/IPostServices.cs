using System.Collections.Generic;
using System.Security.Claims;
using SimpleSocia.Services.Models.Account;
using SimpleSocia.Services.Models.Followers;
using SimpleSocia.Services.Models.Posts;

namespace SimpleSocial.Services.DataServices.PostsServices
{
    public interface IPostServices
    {
        void CreatePost(MyProfileViewModel viewModel);

        ICollection<PostViewModel> GetUserPosts(ClaimsPrincipal user);

        PostViewModel GetPostById(string id);

        SinglePostViewComponentModel GetSinlSinglePostViewComponentModel(string id, ClaimsPrincipal user);

        bool UserCanSeePost(string id, ClaimsPrincipal user);
    }
}
