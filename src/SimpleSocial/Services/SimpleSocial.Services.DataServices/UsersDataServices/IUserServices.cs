using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using SimpleSocia.Services.Models.Users;
using SimpleSocial.Data.Models;

namespace SimpleSocial.Services.DataServices.UsersDataServices
{
    public interface IUserServices
    {
        ICollection<string> GetAllUsernames();

        UserInfoViewModel GetUserInfo(string userId, string currentUserId);

        string GetWallId(string userId);

        ProfilePicture GetUserProfilePicture(string userId);
    }
}
