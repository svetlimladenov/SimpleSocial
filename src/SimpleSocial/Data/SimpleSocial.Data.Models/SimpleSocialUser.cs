using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace SimpleSocial.Data.Models
{
    // Add profile data for application users by adding properties to the SimpleSocialUser class
    public class SimpleSocialUser : IdentityUser
    {
        public SimpleSocialUser()
        {
            Posts = new HashSet<Post>();
            Comments = new HashSet<Comment>();
            UserPages = new HashSet<Page>();
            Likes = new HashSet<UserLike>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfilePictureURL { get; set; }

        public DateTime? BirthDay { get; set; }

        public Gender? Gender { get; set; }

        public string Description { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [ForeignKey("Wall")]
        public string WallId { get; set; }

        public Wall Wall { get; set; }

        public ICollection<Post> Posts { get; set; }
       
        public ICollection<Comment> Comments { get; set; }

        public ICollection<Page> UserPages { get; set; }

        public ICollection<UserLike> Likes { get; set; }
    }
}
