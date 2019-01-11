using System.Collections.Generic;
using SimpleSocia.Services.Models.Users;

namespace SimpleSocial.Services.DataServices.UsersDataServices
{
    public interface IUserServices
    {
        ICollection<string> GetAllUsernames();

        UserInfoViewModel GetUserInfo(string userId, string currentUserId);
    }
}
