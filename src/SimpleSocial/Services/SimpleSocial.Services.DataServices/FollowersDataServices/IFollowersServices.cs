using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using SimpleSocia.Services.Models.Followers;

namespace SimpleSocial.Services.DataServices.FollowersDataServices
{
    public interface IFollowersServices
    {
        ICollection<SimpeUserViewModel> GetUsersToFollow(ClaimsPrincipal user);

        void Follow(string userToFollowId, ClaimsPrincipal user);
    }
}
