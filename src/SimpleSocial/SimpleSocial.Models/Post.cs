using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleSocial.Models
{
    public class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
        }

        public string Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; } = false;

        public int Likes { get; set; }

        [ForeignKey("Wall")]
        public string WallId { get; set; }

        public Wall Wall { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}