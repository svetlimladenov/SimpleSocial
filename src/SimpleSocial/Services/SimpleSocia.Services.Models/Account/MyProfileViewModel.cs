using System.Collections.Generic;
using SimpleSocia.Services.Models.Comments;
using SimpleSocia.Services.Models.Posts;
using SimpleSocial.Data.Models;

namespace SimpleSocia.Services.Models.Account
{
    public class MyProfileViewModel
    {
        public ICollection<PostViewModel> Posts { get; set; }

        public CreatePostInputModel CreatePost { get; set; }

        public ProfilePicture ProfilePicture { get; set; }

        public CommentInputModel CommentInputModel { get; set; }

        public string UserId { get; set; }

        public string WallId { get; set; }

        public bool IsValidProfilePicture { get; set; } = true;
    }
}
