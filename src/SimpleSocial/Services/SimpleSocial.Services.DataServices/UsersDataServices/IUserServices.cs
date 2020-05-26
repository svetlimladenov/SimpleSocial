using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleSocial.Services.Models.Users;

namespace SimpleSocial.Services.DataServices.UsersDataServices
{
    public interface IUserServices
    {
        ICollection<string> GetAllUsernames();

        Task<UserInfoViewModel> GetUserInfo(string userId, string currentUserId);
    }
}
