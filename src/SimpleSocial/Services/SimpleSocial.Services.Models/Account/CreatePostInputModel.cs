using System;
using System.ComponentModel.DataAnnotations;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.Mapping;

namespace SimpleSocial.Services.Models.Account
{
    public class CreatePostInputModel : IMapTo<Post>
    {
        public string Title => string.IsNullOrEmpty(Content) ? string.Empty : Content.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];

        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int WallId { get; set; }

        public int Likes { get; set; }
    }   
}
