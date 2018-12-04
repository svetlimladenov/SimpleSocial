using System.Collections.Generic;
using System.Security.Claims;
using SimpleSocial.Data.Models;

namespace SimpleSocial.Services.DataServices
{
    public interface IMyProfileServices
    {
        ICollection<Post> GetUserPosts(ClaimsPrincipal user);

        string GetWallId(ClaimsPrincipal user);
    }
}
