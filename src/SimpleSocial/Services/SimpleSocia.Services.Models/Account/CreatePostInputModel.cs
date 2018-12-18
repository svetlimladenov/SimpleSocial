using System;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.Mapping;

namespace SimpleSocia.Services.Models.Account
{
    public class CreatePostInputModel : IMapTo<Post>
    {
        public string Title => Content.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
        
        public string Content { get; set; }

        public string UserId { get; set; }

        public string WallId { get; set; }

        public int Likes { get; set; }
        
    }   
}
