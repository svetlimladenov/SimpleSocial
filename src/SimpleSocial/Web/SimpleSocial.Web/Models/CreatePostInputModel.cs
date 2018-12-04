using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleSocial.Web.Models
{
    public class CreatePostInputModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public string WallId { get; set; }

        public int Likes { get; set; }
    }
}
