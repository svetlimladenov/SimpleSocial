using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using SimpleSocia.Services.Models.SignUp;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.Account;
using SimpleSocial.Services.DataServices.UsersDataServices;

namespace SimpleSocial.Services.DataServices.SignUpDetails
{
    public class ProfileDetailsServices : IProfileDetailsServices
    {
        //TODO: DELETE THIS SERVICE !!!
        private readonly IRepository<SimpleSocialUser> userRepository;
        private readonly IUserServices userServices;
        private readonly UserManager<SimpleSocialUser> userManager;

        public ProfileDetailsServices(
            IRepository<SimpleSocialUser> userRepository,
            IUserServices userServices,
            UserManager<SimpleSocialUser> userManager
        )
        {
            this.userRepository = userRepository;
            this.userServices = userServices;
            this.userManager = userManager;
        }

        public void SetNames(ProfileDetailsInputModel inputModel, ClaimsPrincipal user)
        {
            var currentUser = userManager.GetUserAsync(user).GetAwaiter().GetResult();
            if (currentUser == null || string.IsNullOrEmpty(inputModel.FirstName) || string.IsNullOrEmpty(inputModel.LastName))
            {
                return;
            }
            currentUser.FirstName = inputModel.FirstName;
            currentUser.LastName = inputModel.LastName;

            userRepository.SaveChangesAsync().GetAwaiter().GetResult();
        }

        public void SetBirthDay(ProfileDetailsInputModel inputModel, ClaimsPrincipal user)
        {
            var currentUser = userManager.GetUserAsync(user).GetAwaiter().GetResult();
            if (currentUser == null || inputModel.BirthDay == DateTime.MinValue)
            {
                return;
            }
            
            currentUser.BirthDay = inputModel.BirthDay;

            userRepository.SaveChangesAsync().GetAwaiter().GetResult();
        }

        public void SetLivingPlace(ProfileDetailsInputModel inputModel, ClaimsPrincipal user)
        {
            var currentUser = userManager.GetUserAsync(user).GetAwaiter().GetResult();
            if (currentUser == null || string.IsNullOrEmpty(inputModel.Country) || string.IsNullOrEmpty(inputModel.City))
            {
                return;
            }

            currentUser.Country = inputModel.Country;
            currentUser.City = inputModel.City;

            userRepository.SaveChangesAsync().GetAwaiter().GetResult();
        }

        public List<String> GetCounties()
        {
            var CountryList = new List<string>();
            var CInfoList = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (CultureInfo CInfo in CInfoList)
            {
                var R = new RegionInfo(CInfo.LCID);
                if (!(CountryList.Contains(R.EnglishName)))
                {
                    CountryList.Add(R.EnglishName);
                }
            }

            CountryList.Sort();

            return CountryList;
        }
    }
}
