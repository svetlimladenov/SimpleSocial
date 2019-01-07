using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleSocia.Services.Models.Followers;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.Mapping;

namespace SimpleSocial.Web.Areas.Administration.Services
{
    public class AdministrationServices : IAdministrationServices
    {
        private readonly UserManager<SimpleSocialUser> userManager;
        private readonly IRepository<SimpleSocialUser> userRepository;
        private readonly IRepository<PostReport> reportsRepository;

        public AdministrationServices(
            UserManager<SimpleSocialUser> userManager,
            IRepository<SimpleSocialUser> userRepository,
            IRepository<PostReport> reportsRepository)
        {
            this.userManager = userManager;
            this.userRepository = userRepository;
            this.reportsRepository = reportsRepository;
        }


        public ICollection<SimpleUserViewModel> GetAdminUsers(ClaimsPrincipal currentUser, List<SimpleUserViewModel> users = null)
        {
            var usersFound = new List<SimpleSocialUser>();
            if (users == null)
            {
                usersFound = this.userRepository.All().Include(x => x.ProfilePicture).Where(x => x.UserName != currentUser.Identity.Name).Take(10).ToList();
            }
            else
            {
                foreach (var user in users)
                {
                    var userToAdd = new SimpleSocialUser()
                    {
                        Id = user.Id,
                        ProfilePicture = user.ProfilePicture,
                        UserName = user.UserName,
                    };
                    usersFound.Add(userToAdd);
                }
            }
            
            var admins = new List<SimpleSocialUser>();
            foreach (var user in usersFound)
            {
                if (userManager.IsInRoleAsync(user,"Admin").GetAwaiter().GetResult())
                {
                    admins.Add(user);
                }
            }
            var result =  admins.Select(Mapper.Map<SimpleUserViewModel>).ToArray();

            return result;
        }

        public ICollection<SimpleUserViewModel> GetNonAdminUsers(ClaimsPrincipal currentUser, List<SimpleUserViewModel> users = null)
        {
            var usersFound = new List<SimpleSocialUser>();
            if (users == null)
            {
                usersFound = this.userRepository.All().Include(x => x.ProfilePicture).Where(x => x.UserName != currentUser.Identity.Name).Take(20).ToList();
            }
            else
            {
                foreach (var user in users)
                {
                    var userToAdd = new SimpleSocialUser()
                    {
                        Id = user.Id,
                        ProfilePicture = user.ProfilePicture,
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
            var result = nonAdmins.Select(Mapper.Map<SimpleUserViewModel>).ToArray();

            return result;
        }

        public void PromoteUser(string id)
        {
            var user = this.userRepository.All().FirstOrDefault(x => x.Id == id);
            userManager.RemoveFromRoleAsync(user, "User").GetAwaiter().GetResult();
            this.userManager.AddToRoleAsync(user, "Admin").GetAwaiter().GetResult();
        }

        public void DemoteUser(string id)
        {
            var user = this.userRepository.All().FirstOrDefault(x => x.Id == id);
            userManager.RemoveFromRoleAsync(user, "Admin").GetAwaiter().GetResult();
            userManager.AddToRoleAsync(user, "User").GetAwaiter().GetResult();
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

        public ICollection<PostReport> GetAllReports()
        {
            return this.reportsRepository.All().Include(x => x.Author).Include(x => x.Post).ThenInclude(p => p.User).ToList();
        }
    }
}