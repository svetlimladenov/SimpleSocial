using SimpleSocia.Services.Models.Account;
using System.Security.Claims;

namespace SimpleSocial.Services.DataServices.ProfilePictureServices
{
    public interface IProfilePictureService
    {
        void UploadProfilePictureCloudinary(ClaimsPrincipal user, UploadProfilePictureInputModel inputModel);

        public string GetUserProfilePictureURL(string userId);
    }
}
