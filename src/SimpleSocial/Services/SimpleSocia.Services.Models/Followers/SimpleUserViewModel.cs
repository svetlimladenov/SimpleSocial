using SimpleSocial.Data.Models;
using SimpleSocial.Services.Mapping;

namespace SimpleSocia.Services.Models.Followers
{
    public class SimpleUserViewModel : IMapFrom<SimpleSocialUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public ProfilePicture ProfilePicture { get; set; }

        public bool IsFollowingCurrentUser { get; set; }
    }
}