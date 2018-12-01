using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleSocial.Models;

namespace SimpleSocial.ViewModels.Account
{
    public class MyProfileViewModel
    {
        public ICollection<Post> Posts { get; set; }

        public CreatePostInputModel CreatePost { get; set; }

        public string UserId { get; set; }

        public string WallId { get; set; }
    }
}
