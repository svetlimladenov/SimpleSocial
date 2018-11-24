using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleSocial.Models
{
    public class Wall
    {
        public Wall()
        {
            Posts = new HashSet<Post>();
        }

        public string Id { get; set; }

        public string UserId { get; set; }

        public string User { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
