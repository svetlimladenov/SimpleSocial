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
        }

        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; } = false;

        public int Likes { get; set; } = 0;

        public string UserId { get; set; }

        public SimpleSocialUser User { get; set; }

        [ForeignKey("Wall")]
        public string WallId { get; set; }

        public Wall Wall { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<PostReport> PostReports { get; set; }
    }
}
