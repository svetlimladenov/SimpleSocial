using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using SimpleSocia.Services.Models.Posts;
using SimpleSocial.Data.Models;

namespace SimpleSocia.Services.Models.Account
{
    public class MyProfileViewModel
    {
        public IEnumerable<PostViewModel> Posts { get; set; }

        public CreatePostInputModel CreatePost { get; set; }

        public ProfilePicture ProfilePicture { get; set; }

        public string UserId { get; set; }

        public string WallId { get; set; }

    }
}
