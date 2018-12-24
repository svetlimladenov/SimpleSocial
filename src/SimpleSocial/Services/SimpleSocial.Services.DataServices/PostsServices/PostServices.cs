using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleSocia.Services.Models.Account;
using SimpleSocia.Services.Models.Posts;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.Mapping;

namespace SimpleSocial.Services.DataServices.PostsServices
{
    public class PostServices : IPostServices
    {
        private readonly IRepository<Post> postRepository;
        private readonly IRepository<UserLike> userLikesRepository;
        private readonly UserManager<SimpleSocialUser> userManager;

        public PostServices(
            IRepository<Post> postRepository,
            IRepository<UserLike> userLikesRepository,
            UserManager<SimpleSocialUser> userManager
        )
        {
            this.postRepository = postRepository;
            this.userLikesRepository = userLikesRepository;
            this.userManager = userManager;
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

        public ICollection<PostViewModel> GetUserPosts(ClaimsPrincipal user)
        {
            var userId = userManager.GetUserId(user);
            
            var posts = this.postRepository.All().Include(p => p.User).ThenInclude(u => u.ProfilePicture).Include(p => p.Comments).ThenInclude(p => p.Author).ThenInclude(a => a.ProfilePicture).Select(x => Mapper.Map<PostViewModel>(x)).Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedOn).ToList();

            foreach (var post in posts)
            {
                var likes = userLikesRepository.All().Where(x => x.PostId == post.Id).ToList();
                if (likes.FirstOrDefault(x => x.UserId == userId) == null)
                {
                    post.IsLiked = false;
                }
                else
                {
                    post.IsLiked = true;
                }
                post.Likes = likes;
            }
            return posts;
        }


        public PostViewModel GetPostById(string id)
        {
            var post = this.postRepository.All().To<PostViewModel>().FirstOrDefault(x => x.Id == id);
            return post;
        }
    }
}