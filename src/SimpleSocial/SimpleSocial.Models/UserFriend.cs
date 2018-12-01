using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SimpleSocial.Models
{
    public class UserFriend
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public string FriendId { get; set; }

        public User Friend { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
