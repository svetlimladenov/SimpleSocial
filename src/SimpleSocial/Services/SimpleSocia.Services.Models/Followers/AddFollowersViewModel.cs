using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleSocia.Services.Models.Followers
{
    public class AddFollowersViewModel
    {
        public ICollection<SimpeUserViewModel> UsersToFollow { get; set; }
    }
}
