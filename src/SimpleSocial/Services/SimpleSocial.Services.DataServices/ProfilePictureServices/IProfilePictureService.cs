using System.Security.Claims;
using System.Threading.Tasks;
using SimpleSocial.Services.Models.Account;

namespace SimpleSocial.Services.DataServices.ProfilePictureServices
{
    public interface IProfilePictureService
    {
        Task UploadProfilePictureCloudinary(ClaimsPrincipal user, UploadProfilePictureInputModel inputModel);

        public string GetUserProfilePictureURL(string userId);
    }
}
