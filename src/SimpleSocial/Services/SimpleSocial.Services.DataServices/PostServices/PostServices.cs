using System.Threading.Tasks;
using AutoMapper;
using SimpleSocia.Services.Models.Account;
using SimpleSocia.Services.Models.Posts;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;

namespace SimpleSocial.Services.DataServices.PostServices
{
    public class PostServices : IPostServices
    {
        private readonly IRepository<Post> postRepository;

        public PostServices(IRepository<Post> postRepository)
        {
            this.postRepository = postRepository;
        }

        public async Task CreatePost(MyProfileViewModel viewModel)
        {
            var post = new Post
            {
                UserId = viewModel.CreatePost.UserId,
                Title = viewModel.CreatePost.Title,
                WallId = viewModel.CreatePost.WallId,
                Content = viewModel.CreatePost.Content,
            };

            
            await postRepository.AddAsync(post);
            await postRepository.SaveChangesAsync();
        }
    }
}