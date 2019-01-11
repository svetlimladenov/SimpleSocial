using Microsoft.AspNetCore.Identity;
using Shouldly;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.Account;
using Xunit;

namespace SimpleSocial.Services.DataServices.Tests.MyProfileServicesTests
{
    public class MyProfileServicesTests : BaseServiceInitializer
    {
        private IMyProfileServices MyProfileServices => (IMyProfileServices)this.Provider.GetService(typeof(IMyProfileServices));
        private UserManager<SimpleSocialUser> UserManager => (UserManager<SimpleSocialUser>)this.Provider.GetService(typeof(UserManager<SimpleSocialUser>));


        [Fact]
        public void GetUserProfilePic_ShouldWorkFine()
        {
            var user = new SimpleSocialUser
            {
                Id = "test",
                UserName = "TEST",
                ProfilePictureId = "test"
            };
            
            this.UserManager.CreateAsync(user).GetAwaiter();

            this.Context.ProfilePictures.Add(new ProfilePicture()
            {
                UserId = user.Id,
                FileName = "test"
            });
            this.Context.SaveChanges();

            var profPic = this.MyProfileServices.GetUserProfilePicture(user.Id);
            profPic.FileName.ShouldBe("test");
        }

        
    }
}
