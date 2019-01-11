using System;
using Microsoft.EntityFrameworkCore;
using SimpleSocial.Data;

namespace SimpleSocial.Services.DataServices.Tests
{
    public class PostServicesTests
    {
        private readonly SimpleSocialContext dbContext;

        public PostServicesTests()
        {
            var guid = new Guid().ToString();
            var options = new DbContextOptionsBuilder<SimpleSocialContext>()
                .UseInMemoryDatabase(databaseName: guid) // Give a Unique name to the DB
                .Options;
            
            this.dbContext = new SimpleSocialContext(options);

        }

        ////Test using Mock 
        //[Fact]
        //public void GetTotalPostsCountShouldReturnCorrectNumber()
        //{
        //    var jokesRepository = new Mock<IRepository<Post>>();
        //    jokesRepository.Setup(r => r.All()).Returns(new List<Post>
        //    {
        //        new Post(),
        //        new Post(),
        //        new Post()
        //    }.AsQueryable());
        //    var service = new PostServices(jokesRepository.Object,null,null, null, null, null);
        //    Assert.Equal(3,service.GetTotalPostsCount());       
        //}

        //[Fact]
        //public void CreatingTwoPostsShouldReturnCurrectCount()
        //{
        //    var repository = new DbRepository<Post>(dbContext);
        //    var postService = new PostServices(repository, null, null, null, null, null);

        //    postService.CreatePost(new MyProfileViewModel
        //    {
        //        CreatePost = new CreatePostInputModel
        //        {
        //            Content = "Content",
        //            Likes = 0,
        //            UserId = "asdf",
        //            WallId = "sdaf",
        //        }
        //    });

        //    postService.CreatePost(new MyProfileViewModel
        //    {
        //        CreatePost = new CreatePostInputModel
        //        {
        //            Content = "Content",
        //            Likes = 0,
        //            UserId = "asdf",
        //            WallId = "sdaf",
        //        }
        //    });

        //    var count = postService.GetTotalPostsCount();

        //    Assert.Equal(2, count);
        //}


        ////Test using InMemory DB
        //[Fact]
        //public async Task GetTotalPostsCountShouldReturnCorrectNumber()
        //{
        //    dbContext.Posts.Add(new Post());
        //    dbContext.Posts.Add(new Post());
        //    dbContext.Posts.Add(new Post());
        //    await this.dbContext.SaveChangesAsync();

        //    var repository = new DbRepository<Post>(dbContext);
        //    var postService = new PostServices(repository,null,null,null, null,null);
        //    var count = postService.GetTotalPostsCount();
        //    count.ShouldBe(5);
        //}   
    }
}
