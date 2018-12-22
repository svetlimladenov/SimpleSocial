using SimpleSocial.Data.Models;
using SimpleSocial.Services.Mapping;

namespace SimpleSocia.Services.Models.Followers
{
    public class SimpeUserViewModel : IMapFrom<SimpleSocialUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public ProfilePicture ProfilePicture { get; set; }
    }
}