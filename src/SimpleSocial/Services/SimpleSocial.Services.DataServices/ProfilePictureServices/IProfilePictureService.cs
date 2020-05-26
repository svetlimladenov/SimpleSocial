using System.Security.Claims;
using System.Threading.Tasks;
using SimpleSocial.Services.Models.Account;

namespace SimpleSocial.Services.DataServices.ProfilePictureServices
{
    public interface IProfilePictureService
    {
        bool VerifyPicture(UploadProfilePictureInputModel pictureModel);

        Task UploadProfilePictureCloudinary(ClaimsPrincipal user, UploadProfilePictureInputModel inputModel);

    }
}
