using System.Collections.Generic;
using System.Globalization;

namespace SimpleSocial.Services.DataServices.SignUpDetails
{
    public class ProfileDetailsServices : IProfileDetailsServices
    {
        public List<string> GetCounties()
        {
            var CountryList = new List<string>();
            var CInfoList = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (CultureInfo CInfo in CInfoList)
            {
                var R = new RegionInfo(CInfo.Name);
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
