using System;
using System.Collections.Generic;
using System.Text;
using SimpleSocia.Services.Models.Comments;
using SimpleSocial.Data.Models;

namespace SimpleSocia.Services.Models.Posts
{
    public class SinglePostViewComponentModel
    {
        public string PostAuthorId { get; set; }

        public string PostVisitorId { get; set; }

        public PostViewModel Post { get; set; }

        public CommentInputModel CommentInputModel { get; set; }

        public ProfilePicture ProfilePicture { get; set; }

        public string LikeClassName { get; set; }
    }
}
