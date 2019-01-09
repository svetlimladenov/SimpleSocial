using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Shouldly;
using SimpleSocia.Services.Models.Account;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.PostsServices;
using Xunit;

namespace SimpleSocial.Services.DataServices.Tests.Posts
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

            var wall = new Wall()
            {
                Id = "Test"
            };

            var createPostViewModel = new CreatePostInputModel
            {
                Content = "test, test, test",
                UserId = user.Id,
                WallId = wall.Id,
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

            var wall = new Wall()
            {
                Id = "Test"
            };

            var post = new Post() { Id = "Test", UserId = user.Id, WallId = wall.Id };

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

            var wall = new Wall()
            {
                Id = "Test"
            };

            var post = new Post() { Id = "Test", UserId = "otherTest", WallId = wall.Id };

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

            Dispose();
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

        //TODO: Test NEWS FEED POSTS
    }
}
