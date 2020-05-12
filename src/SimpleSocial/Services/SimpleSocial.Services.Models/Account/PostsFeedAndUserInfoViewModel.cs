using System.Collections.Generic;
using SimpleSocial.Services.Models.Comments;
using SimpleSocial.Services.Models.Posts;
using SimpleSocial.Services.Models.Users;

namespace SimpleSocial.Services.Models.Account
{
    public class PostsFeedAndUserInfoViewModel
    {
        public UserInfoViewModel CurrentUserInfo{ get; set; }

        public ICollection<PostViewModel> Posts { get; set; }
        
        public CommentInputModel CommentInputModel { get; set; }

        public UserInfoViewModel UserProfileInfo { get; set; }

        public CreatePostInputModel CreatePost { get; set; }
    }
}