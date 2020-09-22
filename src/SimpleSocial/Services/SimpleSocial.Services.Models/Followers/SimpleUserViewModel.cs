using SimpleSocial.Data.Models;
using SimpleSocial.Services.Mapping;

namespace SimpleSocial.Services.Models.Followers
{
    public class SimpleUserViewModel : IMapFrom<SimpleSocialUser>
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string ProfilePictureURL { get; set; }

        public bool IsFollowingCurrentUser { get; set; }
    }
}