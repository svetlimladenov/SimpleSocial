using System.Collections.Generic;
using System.Security.Claims;
using SimpleSocia.Services.Models.Account;
using SimpleSocia.Services.Models.Posts;
using SimpleSocia.Services.Models.Users;
using SimpleSocial.Data.Models;

namespace SimpleSocial.Services.DataServices.Account
{
    public interface IMyProfileServices
    {
        ProfilePicture GetUserProfilePicture(string id);

        void UploadProfilePictureCloudinary(ClaimsPrincipal user, UploadProfilePictureInputModel inputModel);
    }
}
