using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleSocia.Services.Models.Followers;
using SimpleSocia.Services.Models.Search;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.FollowersDataServices;

namespace SimpleSocial.Services.DataServices.SearchDataServices
{
    public class SearchServices : ISearchServices
    {
        private readonly IRepository<SimpleSocialUser> userRepository;
        private readonly IFollowersServices followersServices;
        private readonly UserManager<SimpleSocialUser> userManager;

        public SearchServices(
            IRepository<SimpleSocialUser> userRepository,
            IFollowersServices followersServices,
            UserManager<SimpleSocialUser> userManager)
        {
            this.userRepository = userRepository;
            this.followersServices = followersServices;
            this.userManager = userManager;
        }

        public SearchResultsViewModel GetResultOfSearch(string searchText, ClaimsPrincipal currentUser)
        {
            var currentUserId = userManager.GetUserId(currentUser);
            var result = new SearchResultsViewModel();
            result.SearchText = searchText;
            var users = userRepository.All().Include(x => x.ProfilePicture).Where(x => x.UserName.Contains(searchText)).Select(x => Mapper.Map<SimpleUserViewModel>(x)).ToList();

            foreach (var user in users)
            {
                user.IsFollowingCurrentUser = this.followersServices.IsBeingFollowedBy(user.Id, currentUserId);
            }

            result.UsersFound = new UsersListViewModel()
            {
                Users = users
            };

            return result;
        }
    }
}
