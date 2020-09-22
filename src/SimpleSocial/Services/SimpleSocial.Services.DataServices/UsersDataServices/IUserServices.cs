using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using SimpleSocial.Services.Models.Users;

namespace SimpleSocial.Services.DataServices.UsersDataServices
{
    public interface IUserServices
    {
        int GetUserId(ClaimsPrincipal claimsPrincipal);

        ICollection<string> GetAllUsernames();

        Task<UserInfoViewModel> GetUserInfo(int userId, int currentUserId);
    }
}
