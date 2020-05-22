using SimpleSocial.Data.Models;
using SimpleSocial.Services.Mapping;

namespace SimpleSocial.Services.Models.Reports
{
    public class ReportAuthorViewModel : IMapFrom<SimpleSocialUser>
    {
        public string Username { get; set; }

        public string ProfilePictureURL { get; set; }
    }
}
