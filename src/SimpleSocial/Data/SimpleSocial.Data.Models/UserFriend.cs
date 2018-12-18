using System;

namespace SimpleSocial.Data.Models
{
    public class UserFriend
    {
        public string UserId { get; set; }

        public SimpleSocialUser User { get; set; }

        public string FriendId { get; set; }

        public SimpleSocialUser Friend { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
