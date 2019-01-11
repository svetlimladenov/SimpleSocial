using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Identity;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.UsersDataServices;

namespace SimpleSocial.Services.DataServices.SignUpDetails
{
    public class ProfileDetailsServices : IProfileDetailsServices
    {
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

        public List<string> GetCounties()
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
