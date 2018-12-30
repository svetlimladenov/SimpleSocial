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

        ICollection<PostViewModel> GetUserPosts(string userId, string currrentUserId);

        PostViewModel GetPostById(string id);

        SinglePostViewComponentModel GetSinglePostViewComponentModel(string id, string visitorId);

        bool UserCanSeePost(string id, ClaimsPrincipal user);
    }
}
