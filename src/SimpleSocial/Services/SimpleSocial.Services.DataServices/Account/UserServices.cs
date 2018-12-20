using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;

namespace SimpleSocial.Services.DataServices.Account
{
    public class UserServices : IUserServices
    {
        private readonly IRepository<SimpleSocialUser> userRepository;
        private readonly UserManager<SimpleSocialUser> userManager;

        public UserServices(
            IRepository<SimpleSocialUser> userRepository,
            UserManager<SimpleSocialUser> userManager)
        {
            this.userRepository = userRepository;
            this.userManager = userManager;
        }
        public SimpleSocialUser GetUser(ClaimsPrincipal user)
        {
            var userId = userManager.GetUserId(user);
            return userRepository.All().FirstOrDefault(x => x.Id == userId);
        }
    }
}