using System.Collections.Generic;
using SimpleSocial.Data.Models;

namespace SimpleSocial.Web.Models
{
    public class MyProfileViewModel
    {
        public ICollection<Post> Posts { get; set; }

        public CreatePostInputModel CreatePost { get; set; }

        public string UserId { get; set; }

        public string WallId { get; set; }

    }
}
