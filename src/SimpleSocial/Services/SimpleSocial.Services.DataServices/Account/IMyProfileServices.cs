using System.Collections.Generic;
using System.Security.Claims;
using SimpleSocia.Services.Models.Account;
using SimpleSocia.Services.Models.Posts;
using SimpleSocial.Data.Models;

namespace SimpleSocial.Services.DataServices.Account
{
    public interface IMyProfileServices
    {
        string GetWallId(ClaimsPrincipal user);

        ProfilePicture GetProfilePicture(ClaimsPrincipal user);

        void UploadProfilePicture(ClaimsPrincipal user, UploadProfilePictureInputModel inputModel, string imgExtension);
    }
}
