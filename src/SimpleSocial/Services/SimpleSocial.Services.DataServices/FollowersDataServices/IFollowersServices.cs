using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using SimpleSocial.Services.Models.Followers;

namespace SimpleSocial.Services.DataServices.FollowersDataServices
{
    public interface IFollowersServices
    {
        Task<IEnumerable<T>> GetUsersToFollow<T>(ClaimsPrincipal user);

        Task<IEnumerable<SimpleUserViewModel>> GetUsersToFollow(ClaimsPrincipal user);

        Task Follow(int userToFollowId, int currentUserId);

        ICollection<SimpleUserViewModel> GetFollowers(ClaimsPrincipal user);

        Task Unfollow(int userId, int currentUserId);

        ICollection<SimpleUserViewModel> GetFollowings(ClaimsPrincipal user);

        bool IsBeingFollowedBy(int userA, int userB);
    }
}
