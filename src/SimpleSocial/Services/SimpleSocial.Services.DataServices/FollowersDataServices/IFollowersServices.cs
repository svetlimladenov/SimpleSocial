using System.Collections.Generic;
using System.Security.Claims;
using SimpleSocia.Services.Models.Followers;

namespace SimpleSocial.Services.DataServices.FollowersDataServices
{
    public interface IFollowersServices
    {
        IEnumerable<SimpleUserViewModel> GetUsersToFollow(ClaimsPrincipal user);

        void Follow(string userToFollowId, ClaimsPrincipal user);

        ICollection<SimpleUserViewModel> GetFollowers(ClaimsPrincipal user);

        void Unfollow(string userId, ClaimsPrincipal user);

        ICollection<SimpleUserViewModel> GetFollowings(ClaimsPrincipal user);

        bool IsBeingFollowedBy(string userA, string userB);
    }
}
