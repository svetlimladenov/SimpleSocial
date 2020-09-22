using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.Models.Account;
using SimpleSocial.Services.Models.Posts;

namespace SimpleSocial.Services.DataServices.PostsServices
{
    public interface IPostServices
    {
        Task CreatePost(MyProfileViewModel viewModel);

        ICollection<PostViewModel> GetUserPosts(int userId, int currrentUserId, int pageNumber);

        PostViewModel GetPostById(int id);

        SinglePostViewComponentModel GetSinglePostViewComponentModel(int id, int visitorId);

        ICollection<PostViewModel> GetNewsFeedPosts(int currrentUserId, int pageNumber);

        SimpleSocialUser GetPostAuthor(int postId);

        void DeletePost(int id, ClaimsPrincipal user);

        bool PostExists(int id);
    }
}
