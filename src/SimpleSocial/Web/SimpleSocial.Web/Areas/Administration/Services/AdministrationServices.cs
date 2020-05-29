using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleSocial.Services.Models.Followers;
using SimpleSocial.Data.Models;
using SimpleSocial.Data;
using System.Threading.Tasks;
using SimpleSocial.Services.Models.Reports;

namespace SimpleSocial.Web.Areas.Administration.Services
{
    public class AdministrationServices : IAdministrationServices
    {
        private readonly IMapper mapper;
        private readonly UserManager<SimpleSocialUser> userManager;
        private readonly SimpleSocialContext dbContext;

        public AdministrationServices(
            IMapper mapper,
            UserManager<SimpleSocialUser> userManager,
            SimpleSocialContext dbContext)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.dbContext = dbContext;
        }


        public ICollection<SimpleUserViewModel> GetAdminUsers(ClaimsPrincipal currentUser, List<SimpleUserViewModel> users = null)
        {
            var usersFound = new List<SimpleSocialUser>();
            if (users == null)
            {
                usersFound = this.dbContext.Users.Where(x => x.UserName != currentUser.Identity.Name).Take(10).ToList();
            }
            else
            {
                foreach (var user in users)
                {
                    var userToAdd = new SimpleSocialUser()
                    {
                        Id = user.Id,
                        ProfilePictureURL = user.ProfilePictureURL,
                        UserName = user.UserName,
                    };
                    usersFound.Add(userToAdd);
                }
            }

            var admins = new List<SimpleSocialUser>();
            foreach (var user in usersFound)
            {
                if (userManager.IsInRoleAsync(user, "Admin").GetAwaiter().GetResult())
                {
                    admins.Add(user);
                }
            }

            var result = mapper.Map<List<SimpleUserViewModel>>(admins);
            return result;
        }

        public ICollection<SimpleUserViewModel> GetNonAdminUsers(ClaimsPrincipal currentUser, List<SimpleUserViewModel> users = null)
        {
            var usersFound = new List<SimpleSocialUser>();
            if (users == null)
            {
                usersFound = this.dbContext.Users.Where(x => x.UserName != currentUser.Identity.Name).Take(20).ToList();
            }
            else
            {
                foreach (var user in users)
                {
                    var userToAdd = new SimpleSocialUser()
                    {
                        Id = user.Id,
                        ProfilePictureURL = user.ProfilePictureURL,
                        UserName = user.UserName,
                    };
                    usersFound.Add(userToAdd);
                }
            }

            var nonAdmins = new List<SimpleSocialUser>();
            foreach (var user in usersFound)
            {
                if (!userManager.IsInRoleAsync(user, "Admin").GetAwaiter().GetResult())
                {
                    nonAdmins.Add(user);
                }
            }

            var result = mapper.Map<List<SimpleUserViewModel>>(nonAdmins);

            return result;
        }

        public async Task PromoteUser(string id)
        {
            var user = await this.dbContext.Users.FindAsync(id);
            await userManager.RemoveFromRoleAsync(user, "User");
            await this.userManager.AddToRoleAsync(user, "Admin");
        }

        public async Task DemoteUser(string id)
        {
            var user = await this.dbContext.Users.FindAsync(id);
            await userManager.RemoveFromRoleAsync(user, "Admin");
            await userManager.AddToRoleAsync(user, "User");
        }

        public string GetRandomQuote()
        {
            var motivationQuotes = new string[]
            {
                "The Way Get Started Is To Quit Talking And Begin Doing.",
                "The Pessimist Sees Difficulty In Every Opportunity. The Optimist Sees Opportunity In Every Difficulty.",
                "You Learn More From Failure Than From Success. Don’t Let It Stop You. Failure Builds Character.",
                "If You Are Working On Something That You Really Care About, You Don’t Have To Be Pushed. The Vision Pulls You.",
                "People Who Are Crazy Enough To Think They Can Change The World, Are The Ones Who Do.",
                "Failure Will Never Overtake Me If My Determination To Succeed Is Strong Enough.",
            };
            var randomNumber = new Random().Next(0, motivationQuotes.Length - 1);
            return motivationQuotes[randomNumber];
        }

        public async Task<MinifiedPostsListViewModel> GetAllReports(int pageNumber, int numberOfPosts = 10)
        {
            var reports = await this.dbContext.PostReports
                .Skip(pageNumber * numberOfPosts)
                .Take(numberOfPosts)
                .Select(x => new MinifiedPostViewModel
                {
                    Id = x.Id,
                    PostAuthorId = x.Post.User.Id,
                    PostAuthorUsername = x.Post.User.UserName,
                    ReportAuthorId = x.AuthorId,
                    ReportAuthorUsername = x.Author.UserName,
                    ReportReason = x.ReportReason.ToString()
                }).ToListAsync();

            var result = new MinifiedPostsListViewModel()
            {
                Reports = reports
            };

            return result;
        }
    }
}