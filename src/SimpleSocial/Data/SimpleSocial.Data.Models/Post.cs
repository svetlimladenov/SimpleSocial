using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleSocial.Data.Models
{
    public class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            PostReports = new HashSet<PostReport>();
            Likes = new HashSet<UserLike>();
        }

        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; } = false;

        public ICollection<UserLike> Likes { get; set; }

        public string UserId { get; set; }

        public SimpleSocialUser User { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<PostReport> PostReports { get; set; }
    }
}
