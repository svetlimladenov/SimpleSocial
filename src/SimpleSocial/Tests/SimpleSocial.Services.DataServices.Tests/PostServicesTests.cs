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
                .UseInMemoryDatabase(databaseName: guid)
                .Options;
            
            this.dbContext = new SimpleSocialContext(options);

        }    
    }
}
