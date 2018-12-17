using SimpleSocial.Data.Models;
using SimpleSocial.Services.Mapping;

namespace SimpleSocia.Services.Models.Comments
{
    public class CommentInputModel : IMapTo<Comment>
    {
        public string PostId { get; set; }

        public Post Post { get; set; }

        public string AuthorId { get; set; }

        public SimpleSocialUser Author { get; set; }

        public string CommentText { get; set; }
    }
}