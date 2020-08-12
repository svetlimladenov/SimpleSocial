using System.Linq;
using Microsoft.AspNetCore.Identity;
using Shouldly;
using SimpleSocial.Services.Models.Account;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.PostsServices;
using Xunit;

namespace SimpleSocial.Services.DataServices.Tests.PostsServicesTests
{
    public class PostsServicesTests : BaseServiceInitializer
    {
        private IPostServices PostServices => (IPostServices)this.Provider.GetService(typeof(IPostServices));
        private IRepository<Post> PostsRepository => (IRepository<Post>)this.Provider.GetService(typeof(IRepository<Post>));

        [Fact]
        public void CreatePost_ShouldWorkFine()
        {
            var user = new SimpleSocialUser
            {
                Id = "test",
                UserName = "CommentsTest"
            };

            var createPostViewModel = new CreatePostInputModel
            {
                Content = "test, test, test",
                UserId = user.Id
            };

            this.PostServices.CreatePost(new MyProfileViewModel
            {
                CreatePost = createPostViewModel
            });

            var postsCount = PostsRepository.All().Count();

            postsCount.ShouldBe(1);
        }

        [Fact]
        public void DeletePostWhereUserIsTheCreator_ShoudWorkFine()
        {
            var user = new SimpleSocialUser
            {
                Id = "test",
                UserName = "CommentsTest",
            };

            var post = new Post() { Id = "Test", UserId = user.Id };

            //add the new post
            this.PostsRepository.AddAsync(post).GetAwaiter().GetResult();
            this.PostsRepository.SaveChangesAsync().GetAwaiter().GetResult();

            //create the test user
            var userManager = (UserManager<SimpleSocialUser>)this.Provider.GetService(typeof(UserManager<SimpleSocialUser>));
            userManager.CreateAsync(user).GetAwaiter();

            //create the claims principal user thats needed in the test.
            var signInManager = (SignInManager<SimpleSocialUser>)this.Provider.GetService(typeof(SignInManager<SimpleSocialUser>));
            var claims = signInManager.CreateUserPrincipalAsync(user).GetAwaiter().GetResult();

            //delete the post
            this.PostServices.DeletePost(post.Id, claims);

            //chech if its deleted
            this.PostsRepository.All().Count().ShouldBe(0);
        }

        [Fact]
        public void DeletePostWhereUserIsntTheCreator_ShoudWorkFine()
        {
            var user = new SimpleSocialUser
            {
                Id = "test",
                UserName = "CommentsTest",
            };

            var post = new Post() { Id = "Test", UserId = "otherTest" };

            //add the new post
            this.PostsRepository.AddAsync(post).GetAwaiter().GetResult();
            this.PostsRepository.SaveChangesAsync().GetAwaiter().GetResult();

            //create the test user
            var userManager = (UserManager<SimpleSocialUser>)this.Provider.GetService(typeof(UserManager<SimpleSocialUser>));
            userManager.CreateAsync(user).GetAwaiter();

            //create the claims principal user thats needed in the test.
            var signInManager = (SignInManager<SimpleSocialUser>)this.Provider.GetService(typeof(SignInManager<SimpleSocialUser>));
            var claims = signInManager.CreateUserPrincipalAsync(user).GetAwaiter().GetResult();

            //delete the post
            this.PostServices.DeletePost(post.Id, claims);

            //check if its deleted
            this.PostsRepository.All().Count().ShouldBe(1);
        }

        [Fact]
        public void GetUserPosts_ShouldReturnAll()
        {
            var user = new SimpleSocialUser
            {
                Id = "test",
                UserName = "CommentsTest",
            };

            var user2 = new SimpleSocialUser
            {
                Id = "secondUser",
                UserName = "SecondUser"
            };

            var userManager = (UserManager<SimpleSocialUser>)this.Provider.GetService(typeof(UserManager<SimpleSocialUser>));
            userManager.CreateAsync(user).GetAwaiter();
            userManager.CreateAsync(user2).GetAwaiter();
            var post = new MyProfileViewModel
            {
                CreatePost = new CreatePostInputModel()
                {
                    UserId = user.Id,
                    Content = "Post",
                    WallId = "wallId",
                }
            };

            var postForSecondUser = new MyProfileViewModel
            {
                CreatePost = new CreatePostInputModel()
                {
                    UserId = user2.Id,
                    Content = "Post2",
                    WallId = "wallId2",
                }
            };

            this.PostServices.CreatePost(postForSecondUser);
            this.PostServices.CreatePost(post);
            this.PostServices.CreatePost(post);
            this.PostServices.CreatePost(post);

            var userPostsCount = this.PostServices.GetUserPosts(user.Id, "1", 0).Count;
            userPostsCount.ShouldBe(3);
        }

        [Fact]
        public void GetPostAuthor_ShouldWorkFine()
        {
            var author = new SimpleSocialUser
            {
                Id = "test",
                UserName = "CommentsTest",
            };

            var userManager = (UserManager<SimpleSocialUser>)this.Provider.GetService(typeof(UserManager<SimpleSocialUser>));
            userManager.CreateAsync(author).GetAwaiter();

            var post = new MyProfileViewModel
            {
                CreatePost = new CreatePostInputModel()
                {
                    UserId = author.Id,
                    Content = "Post",
                    WallId = "wallId",
                }
            };

            this.PostServices.CreatePost(post);

            var postId = this.PostsRepository.All().FirstOrDefault(x => x.UserId == author.Id).Id;

            var postAuthor = this.PostServices.GetPostAuthor(postId);

            postAuthor.Id.ShouldBe(author.Id);
        }

        [Fact]
        public void CheckIfPostExists_ShoudReturnFalse()
        {
            var exists = this.PostServices.PostExists("no");
            exists.ShouldBe(false);
        }

        [Fact]
        public void CheckIfPostExists_ShoudReturnTrue()
        {
            var post = new Post()
            {
                Content = "test",
                UserId = "test"
            };

            this.Context.Posts.Add(post);
            this.Context.SaveChanges();
            var exists = this.PostServices.PostExists(post.Id);
            exists.ShouldBe(true);
        }

        [Fact]
        public void GetPostById_ShuldWorkFine()
        {
            var user = new SimpleSocialUser
            {
                Id = "test",
                UserName = "TEST",
                ProfilePictureURL = "test"
            };
            var userManager = (UserManager<SimpleSocialUser>)this.Provider.GetService(typeof(UserManager<SimpleSocialUser>));
            userManager.CreateAsync(user).GetAwaiter();

            var post = new Post()
            {
                Content = "test",
                UserId = user.Id
            };
            
            this.Context.Posts.Add(post);
            this.Context.SaveChanges();
            var postFromService =  this.PostServices.GetPostById(post.Id);
            postFromService.Id.ShouldBe(post.Id);
        }

        [Fact]
        public void GetSinglePostViewModel_ShouldWorkFine()
        {
            var user = new SimpleSocialUser
            {
                Id = "test",
                UserName = "TEST",
                ProfilePictureURL = "test"
            };
            var userManager = (UserManager<SimpleSocialUser>)this.Provider.GetService(typeof(UserManager<SimpleSocialUser>));
            userManager.CreateAsync(user).GetAwaiter();

            var post = new Post()
            {
                Content = "test",
                UserId = user.Id,
            };

            this.Context.Posts.Add(post);
            this.Context.SaveChanges();

            var viewModel = this.PostServices.GetSinglePostViewComponentModel(post.Id, user.Id);

            viewModel.PostAuthorId.ShouldBe("test");
        }

        [Fact]
        public void GetNewsFeedPosts_ShouldWorkFine()
        {
            var user = new SimpleSocialUser
            {
                Id = "test",
                UserName = "TEST",
                ProfilePictureURL = "test"
            };
            var userManager = (UserManager<SimpleSocialUser>)this.Provider.GetService(typeof(UserManager<SimpleSocialUser>));
            userManager.CreateAsync(user).GetAwaiter();
            var user2 = new SimpleSocialUser
            {
                Id = "test2",
                UserName = "TEST",
                ProfilePictureURL = "test"
            };

            userManager.CreateAsync(user2).GetAwaiter();

            var post = new Post()
            {
                Content = "test",
                UserId = user.Id
            };

            this.Context.Posts.Add(post);
            this.Context.SaveChanges();

            //test2 follova test
            var userFollower = new UserFollower()
            {
                UserId = user.Id,
                FollowerId = user2.Id,
            };

            this.Context.UserFollowers.Add(userFollower);
            this.Context.SaveChanges();

            var posts = this.PostServices.GetNewsFeedPosts(user2.Id, 0).ToArray();
            var firstPostNewsFeed = posts[0];

            firstPostNewsFeed.Id.ShouldBe(post.Id);
        }

        //TODO: Test NEWS FEED POSTS
    }
}
