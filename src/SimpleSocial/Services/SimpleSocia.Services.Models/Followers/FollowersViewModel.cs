using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleSocia.Services.Models.Followers
{
    public class FollowersViewModel
    {
        public ICollection<SimpeUserViewModel> Users { get; set; }
    }
}
