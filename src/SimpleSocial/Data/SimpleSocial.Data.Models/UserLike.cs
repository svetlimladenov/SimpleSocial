using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleSocial.Data.Models
{
    public class UserLike
    {
        public string UserId { get; set; }

        public SimpleSocialUser User { get; set; }

        public string PostId { get; set; }

        public Post Post { get; set; }

        public DateTime LikedOn { get; set; } = DateTime.UtcNow;
    }
}
