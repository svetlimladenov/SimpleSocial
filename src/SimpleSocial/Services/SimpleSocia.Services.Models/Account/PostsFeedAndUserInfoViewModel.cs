using System.Collections.Generic;
using SimpleSocia.Services.Models.Comments;
using SimpleSocia.Services.Models.Posts;
using SimpleSocia.Services.Models.Users;
using SimpleSocial.Data.Models;

namespace SimpleSocia.Services.Models.Account
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