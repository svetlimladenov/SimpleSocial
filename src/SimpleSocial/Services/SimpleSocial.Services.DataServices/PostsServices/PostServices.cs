using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleSocial.Data;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.Mapping;
using SimpleSocial.Services.Models.Account;
using SimpleSocial.Services.Models.Comments;
using SimpleSocial.Services.Models.Followers;
using SimpleSocial.Services.Models.Posts;

namespace SimpleSocial.Services.DataServices.PostsServices
{
    public class PostServices : IPostServices
    {
        private readonly IRepository<Post> postRepository;
        private readonly IRepository<UserLike> userLikesRepository;
        private readonly IRepository<SimpleSocialUser> userRepository;
        private readonly IRepository<UserFollower> userFollowersRepository;
        private readonly IRepository<PostReport> reportsRepository;
        private readonly UserManager<SimpleSocialUser> userManager;
        private readonly SimpleSocialContext dbContext;
        private readonly IMapper mapper;

        public PostServices(
            IRepository<Post> postRepository,
            IRepository<UserLike> userLikesRepository,
            IRepository<SimpleSocialUser> userRepository,
            IRepository<UserFollower> userFollowersRepository,
            IRepository<PostReport> reportsRepository,
            UserManager<SimpleSocialUser> userManager,
            SimpleSocialContext dbContext,
            IMapper mapper
        )
        {
            this.postRepository = postRepository;
            this.userLikesRepository = userLikesRepository;
            this.userRepository = userRepository;
            this.userFollowersRepository = userFollowersRepository;
            this.reportsRepository = reportsRepository;
            this.userManager = userManager;
            this.dbContext = dbContext;
            this.mapper = mapper;
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

            await dbContext.AddAsync(post);
            await dbContext.SaveChangesAsync();

        }

        public ICollection<PostViewModel> GetUserPosts(string userId, string currrentUserId, int pageNumber)
        {
            var posts = this.postRepository
                            .All()
                            .Include(p => p.Likes)
                            .ThenInclude(l => l.User)
                            .Include(p => p.User)
                            .Include(p => p.Comments)
                            .ThenInclude(p => p.Author)
                            .Where(x => x.UserId == userId)
                            .OrderByDescending(x => x.CreatedOn)
                            .Select(x => mapper.Map<PostViewModel>(x))
                            .ToList();

            foreach (var post in posts)
            {
                var likes = userLikesRepository.All().Where(x => x.PostId == post.Id).Select(x => x.User).To<SimpleUserViewModel>().ToList();
                foreach (var liker in likes)
                {
                    liker.IsFollowingCurrentUser = this.IsBeingFollowedBy(liker.Id, currrentUserId);
                }
                if (likes.FirstOrDefault(x => x.Id == currrentUserId) == null)
                {
                    post.IsLiked = false;
                }
                else
                {
                    post.IsLiked = true;
                }
                post.Likes = likes;
            }

            posts = posts.Skip(pageNumber * 20).Take(20).ToList();

            return posts;
        }


        public PostViewModel GetPostById(string id)
        {
            var post = this.dbContext.Posts.Where(x => x.Id == id).To<PostViewModel>().FirstOrDefault();
            if (post == null)
            {
                return null;
            }
            return post;
        }

        public SinglePostViewComponentModel GetSinglePostViewComponentModel(string id, string visitorId)
        {
            var viewModel = new SinglePostViewComponentModel();
            var post = this.GetPostById(id);
            if (post == null)
            {
                return null;
            }
            var likes = userLikesRepository.All().Where(x => x.PostId == post.Id).Select(x => x.User).To<SimpleUserViewModel>().ToList();
            foreach (var liker in likes)
            {
                liker.IsFollowingCurrentUser = this.IsBeingFollowedBy(liker.Id, visitorId);
            }
            post.Likes = likes;
            var postAuthorId = post.User.Id;
            if (post.Likes.FirstOrDefault(x => x.Id == visitorId) == null)
            {
                post.IsLiked = false;
            }
            else
            {
                post.IsLiked = true;
            }

            viewModel.Post = post;
            viewModel.CommentInputModel = new CommentInputModel();
            viewModel.PostVisitorId = visitorId;
            viewModel.PostAuthorId = postAuthorId;
            return viewModel;
        }

        public ICollection<PostViewModel> GetNewsFeedPosts(string currrentUserId, int pageNumber)
        {
            var posts = new List<PostViewModel>();
            var followings = this.userFollowersRepository.All().Where(x => x.FollowerId == currrentUserId);
            foreach (var user in followings)
            {
                var userPosts = this.postRepository.All().Include(x => x.Likes).ThenInclude(x => x.User).Include(x => x.Comments).ThenInclude(x => x.Author).Include(x => x.User).Where(x => x.UserId == user.UserId).Select(x => mapper.Map<Post, PostViewModel>(x));
                foreach (var post in userPosts)
                {
                    posts.Add(post);
                }
            }

            posts = posts.OrderByDescending(x => x.CreatedOn).ToList();

            foreach (var post in posts)
            {
                var likes = userLikesRepository.All().Where(x => x.PostId == post.Id).Select(x => x.User).MapToList<SimpleUserViewModel>();
                foreach (var liker in likes)
                {
                    liker.IsFollowingCurrentUser = this.IsBeingFollowedBy(liker.Id, currrentUserId);
                }
                if (likes.FirstOrDefault(x => x.Id == currrentUserId) == null)
                {
                    post.IsLiked = false;
                }
                else
                {
                    post.IsLiked = true;
                }
                post.Likes = likes;
            }

            posts = posts.Skip(pageNumber * 20).Take(20).ToList();

            return posts;
        }

        public SimpleSocialUser GetPostAuthor(string postId)
         => this.dbContext.Posts.Include(x => x.User).FirstOrDefault(x => x.Id == postId).User;

        public void DeletePost(string id, ClaimsPrincipal user)
        {
            var post = this.postRepository.All().FirstOrDefault(x => x.Id == id);
            var currentUser = userManager.GetUserAsync(user).GetAwaiter().GetResult();
            var isCurrentUserAdmin = userManager.IsInRoleAsync(currentUser, "Admin").GetAwaiter().GetResult();

            if (post == null)
            {
                return;
            }

            if (post.UserId != currentUser.Id && isCurrentUserAdmin == false)
            {
                return;
            }
            var report = this.reportsRepository.All().FirstOrDefault(x => x.PostId == id);
            if (report != null)
            {
                this.reportsRepository.Delete(report);
                this.reportsRepository.SaveChangesAsync().GetAwaiter().GetResult();
            }

            this.postRepository.Delete(post);
            this.postRepository.SaveChangesAsync().GetAwaiter().GetResult();
        }

        public bool PostExists(string id)
        {
            if (this.postRepository.All().Any(p => p.Id == id))
            {
                return true;
            }

            return false;

        }

        public bool IsBeingFollowedBy(string userA, string userB)
        {
            var userAid = userA;
            var userBid = userB;
            return this.userFollowersRepository.All().FirstOrDefault(x => x.UserId == userAid && x.FollowerId == userBid) != null;
        }
    }
}