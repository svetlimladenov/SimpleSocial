using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace SimpleSocial.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            UserFriends = new HashSet<UserFriend>();
            Posts = new HashSet<Post>();
            Comments = new HashSet<Comment>();
            UserPages = new HashSet<Page>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfilePictureUrl { get; set; }

        public DateTime BirthDay { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [ForeignKey("Wall")]
        public string WallId { get; set; }

        public Wall Wall { get; set; }

        public ICollection<Post> Posts { get; set; }

        public ICollection<UserFriend> UserFriends { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Page> UserPages { get; set; }
    }
}
