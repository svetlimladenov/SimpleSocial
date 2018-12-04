using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleSocial.Data.Models
{
    public class Wall
    {
        public Wall()
        {
            Posts = new HashSet<Post>();
        }

        public string Id { get; set; }

        public string UserId { get; set; }

        public SimpleSocialUser User { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
