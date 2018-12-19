using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleSocial.Data.Models
{
    public class PostReport
    {
        public string Id { get; set; }

        public string AuthorId { get; set; }

        public SimpleSocialUser Author { get; set; }

        public string PostId { get; set; }

        public Post Post { get; set; }
    }
}
