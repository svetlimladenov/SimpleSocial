using System.Linq;
using System.Threading.Tasks;
using SimpleSocia.Services.Models.Account;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;

namespace SimpleSocial.Services.DataServices.PostsServices
{
    public class PostServices : IPostServices
    {
        private readonly IRepository<Post> postRepository;

        public PostServices(IRepository<Post> postRepository)
        {
            this.postRepository = postRepository;
        }

        public void CreatePost(MyProfileViewModel viewModel)
        {
            var post = new Post
            {
                UserId = viewModel.CreatePost.UserId,
                Title = viewModel.CreatePost.Title,
                WallId = viewModel.CreatePost.WallId,
                Content = viewModel.CreatePost.Content,
            };

            
            postRepository.AddAsync(post).GetAwaiter().GetResult();
            postRepository.SaveChangesAsync().GetAwaiter().GetResult();
        }

        public int GetTotalPostsCount()
        {
            return this.postRepository.All().Count();
        }
    }
}