using System.Collections.Generic;
using System.Security.Claims;
using SimpleSocia.Services.Models.Posts;
using SimpleSocial.Data.Models;

namespace SimpleSocial.Services.DataServices.Account
{
    public interface IMyProfileServices
    {
        IEnumerable<PostViewModel> GetUserPosts(ClaimsPrincipal user);

        string GetWallId(ClaimsPrincipal user);

        ProfilePicture GetProfilePicture(ClaimsPrincipal user);
    }
}
