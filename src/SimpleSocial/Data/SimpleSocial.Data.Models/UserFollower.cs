using System;

namespace SimpleSocial.Data.Models
{
    public class UserFollower
    {
        public string UserId { get; set; }

        public SimpleSocialUser User { get; set; }

        public string FollowerId { get; set; }

        public SimpleSocialUser Follower { get; set; }

        public DateTime? FollowedDate { get; set; } = DateTime.UtcNow;

        public DateTime? UnfollowedDate { get; set; }
    }
}
