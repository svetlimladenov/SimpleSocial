using System.Security.Claims;
using SimpleSocia.Services.Models.Account;
using SimpleSocial.Data.Models;

namespace SimpleSocial.Services.DataServices.Account
{
    public interface IMyProfileServices
    {
        ProfilePicture GetUserProfilePicture(string id);

        void UploadProfilePictureCloudinary(ClaimsPrincipal user, UploadProfilePictureInputModel inputModel);
    }
}
