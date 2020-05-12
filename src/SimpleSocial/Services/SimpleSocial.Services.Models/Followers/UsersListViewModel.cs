using System.Collections.Generic;

namespace SimpleSocial.Services.Models.Followers
{
    public class UsersListViewModel
    {
        public IEnumerable<SimpleUserViewModel> Users { get; set; } = new HashSet<SimpleUserViewModel>();

        public string NoUsersWord { get; set; }

        public int UsersCount { get; set; } = 0;
    }
}
