using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleSocial.Data.Models
{
    public class UserFollower
    {
        public string UserId { get; set; }

        public SimpleSocialUser User { get; set; }

        public string FollowerId { get; set; }

        public SimpleSocialUser Follower { get; set; }
    }
}
