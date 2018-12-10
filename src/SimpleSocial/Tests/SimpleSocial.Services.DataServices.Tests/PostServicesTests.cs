using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.PostsServices;
using Moq;
using Xunit;

namespace SimpleSocial.Services.DataServices.Tests
{
    public class PostServicesTests
    {
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
    }
}
