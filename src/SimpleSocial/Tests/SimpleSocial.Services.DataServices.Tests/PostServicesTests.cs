using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.PostsServices;
using Moq;
using SimpleSocia.Services.Models.Account;
using SimpleSocial.Data;
using Xunit;

namespace SimpleSocial.Services.DataServices.Tests
{
    public class PostServicesTests
    {
        //Test using Mock 
        [Fact]
        public void GetTotalPostsCountShouldReturnCorrectNumber()
        {
            var jokesRepository = new Mock<IRepository<Post>>();
            jokesRepository.Setup(r => r.All()).Returns(new List<Post>
            {
                new Post(),
                new Post(),
                new Post()
            }.AsQueryable());
            var service = new PostServices(jokesRepository.Object);
            Assert.Equal(3,service.GetTotalPostsCount());       
        }


        //Test using InMemory DB
        //[Fact]
        //public async Task GetTotalPostsCountShouldReturnCorrectNumberUsingDbContext()
        //{
        //    var options = new DbContextOptionsBuilder<SimpleSocialContext>()
        //        .UseInMemoryDatabase(databaseName: "In_Memory_Db") // Give a Unique name to the DB
        //        .Options;
        //    var dbContext = new SimpleSocialContext(options);
        //    dbContext.Posts.Add(new Post());
        //    dbContext.Posts.Add(new Post());
        //    dbContext.Posts.Add(new Post());
        //    await dbContext.SaveChangesAsync();

        //    var repository = new DbRepository<Post>(dbContext);
        //    var postService = new PostServices(repository);
        //    var count = postService.GetTotalPostsCount();
        //    Assert.Equal(3,count);
        //}

        //[Fact]
        //public async Task AddingTwoPostsShouldReturnCurrectCount()
        //{
        //    var options = new DbContextOptionsBuilder<SimpleSocialContext>()
        //        .UseInMemoryDatabase(databaseName: "In_Memory_Db") // Give a Unique name to the DB
        //        .Options;
        //    var dbContext = new SimpleSocialContext(options);

        //    var repository = new DbRepository<Post>(dbContext);
        //    var postService = new PostServices(repository);

        //    await postService.CreatePostAsync(new MyProfileViewModel
        //    {
        //        CreatePost = new CreatePostInputModel
        //        {
        //            Content = "Content",
        //            Title = "Title",
        //            Likes = 0,
        //            UserId = "asdf",
        //            WallId = "sdaf",
        //        }
        //    });
        //    await postService.CreatePostAsync(new MyProfileViewModel
        //    {
        //        CreatePost = new CreatePostInputModel
        //        {
        //            Content = "Content",
        //            Title = "Title",
        //            Likes = 0,
        //            UserId = "asdf",
        //            WallId = "sdaf",
        //        }
        //    });

        //    var count = postService.GetTotalPostsCount();

        //    Assert.Equal(2,count);
        //}
    }
}
