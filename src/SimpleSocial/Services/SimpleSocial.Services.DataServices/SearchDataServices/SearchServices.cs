using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleSocial.Data;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.FollowersDataServices;
using SimpleSocial.Services.Mapping;
using SimpleSocial.Services.Models.Followers;
using SimpleSocial.Services.Models.Search;

namespace SimpleSocial.Services.DataServices.SearchDataServices
{
    public class SearchServices : ISearchServices
    {
        private readonly IFollowersServices followersServices;
        private readonly UserManager<SimpleSocialUser> userManager;
        private readonly SimpleSocialContext dbContext;

        public SearchServices(
            IFollowersServices followersServices,
            UserManager<SimpleSocialUser> userManager,
            SimpleSocialContext dbContext)
        {
            this.followersServices = followersServices;
            this.userManager = userManager;
            this.dbContext = dbContext;
        }

        public SearchResultsViewModel GetResultOfSearch(string searchText, ClaimsPrincipal currentUser)
        {
            var currentUserId = userManager.GetUserId(currentUser);
            var result = new SearchResultsViewModel
            {
                SearchText = searchText
            };
            var users = this.dbContext.Users.Where(x => x.UserName.Contains(searchText)).MapToList<SimpleUserViewModel>();

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
