using SimpleSocial.Data.Models;
using SimpleSocial.Services.Mapping;

namespace SimpleSocial.Services.Models.Comments
{
    public class CommentAuthorViewModel : IMapFrom<SimpleSocialUser>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string ProfilePictureUrl { get; set; }
    }
}
