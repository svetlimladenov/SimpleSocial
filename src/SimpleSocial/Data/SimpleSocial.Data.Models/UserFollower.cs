using System;

namespace SimpleSocial.Data.Models
{
    public class UserFollower
    {
        public int UserId { get; set; }

        public SimpleSocialUser User { get; set; }

        public int FollowerId { get; set; }

        public SimpleSocialUser Follower { get; set; }

        public DateTime? FollowedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UnfollowedDate { get; set; }
    }
}
