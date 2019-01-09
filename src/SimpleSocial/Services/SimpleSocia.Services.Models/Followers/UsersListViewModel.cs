using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleSocia.Services.Models.Followers
{
    public class UsersListViewModel
    {
        public IEnumerable<SimpleUserViewModel> Users { get; set; } = new HashSet<SimpleUserViewModel>();

        public string NoUsersWord { get; set; }

        public int UsersCount { get; set; } = 0;
    }
}
