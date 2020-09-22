using System;

namespace SimpleSocial.Data.Models
{
    public class UserLike
    {
        public int UserId { get; set; }

        public SimpleSocialUser User { get; set; }

        public int PostId { get; set; }

        public Post Post { get; set; }

        public DateTime LikedOn { get; set; } = DateTime.UtcNow;
    }
}
