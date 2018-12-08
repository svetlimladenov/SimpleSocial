using System;
using System.Linq;
using AutoMapper;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.Mapping;

namespace SimpleSocia.Services.Models.Account
{
    public class CreatePostInputModel : IMapTo<Post>
    {
        public string Title { get; set; }

        public string CustomTitle { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public string WallId { get; set; }

        public int Likes { get; set; }
        
    }   
}
