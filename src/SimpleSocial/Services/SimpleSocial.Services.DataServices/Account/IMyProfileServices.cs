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
        string GetWallId(ClaimsPrincipal user);

        ProfilePicture GetProfilePicture(ClaimsPrincipal user);

        void UploadProfilePictureCloudinary(ClaimsPrincipal user, UploadProfilePictureInputModel inputModel);
        
        UserInfoViewModel GetUserInfo(ClaimsPrincipal user);
    }
}
