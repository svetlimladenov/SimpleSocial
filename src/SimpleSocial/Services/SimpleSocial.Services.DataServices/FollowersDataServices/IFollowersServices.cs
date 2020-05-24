using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using SimpleSocial.Services.Models.Followers;

namespace SimpleSocial.Services.DataServices.FollowersDataServices
{
    public interface IFollowersServices
    {
        Task<IEnumerable<SimpleUserViewModel>> GetUsersToFollow(ClaimsPrincipal user);

        Task Follow(string userToFollowId, string currentUserId);

        ICollection<SimpleUserViewModel> GetFollowers(ClaimsPrincipal user);

        Task Unfollow(string userId, string currentUserId);

        ICollection<SimpleUserViewModel> GetFollowings(ClaimsPrincipal user);

        bool IsBeingFollowedBy(string userA, string userB);
    }
}
