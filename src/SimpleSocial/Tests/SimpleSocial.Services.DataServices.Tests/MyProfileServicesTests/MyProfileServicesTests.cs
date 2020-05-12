using Microsoft.AspNetCore.Identity;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.Account;

namespace SimpleSocial.Services.DataServices.Tests.MyProfileServicesTests
{
    public class MyProfileServicesTests : BaseServiceInitializer
    {
        private IMyProfileServices MyProfileServices => (IMyProfileServices)this.Provider.GetService(typeof(IMyProfileServices));
        private UserManager<SimpleSocialUser> UserManager => (UserManager<SimpleSocialUser>)this.Provider.GetService(typeof(UserManager<SimpleSocialUser>));

        
    }
}
