using SimpleSocial.Services.Models.Comments;

namespace SimpleSocial.Services.Models.Posts
{
    public class SinglePostViewComponentModel
    {
        public int PostAuthorId { get; set; }

        public int PostVisitorId { get; set; }

        public PostViewModel Post { get; set; }

        public CommentInputModel CommentInputModel { get; set; }

        public string ProfilePictureURL { get; set; }
        
    }
}
