using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleSocia.Services.Models.Followers
{
    public class UsersListViewModel
    {
        public ICollection<SimpleUserViewModel> Users { get; set; } = new HashSet<SimpleUserViewModel>();

        public string NoUsersWord { get; set; }
    }
}
