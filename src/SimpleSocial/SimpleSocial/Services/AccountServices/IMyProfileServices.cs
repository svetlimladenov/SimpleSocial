using System.Collections.Generic;
using SimpleSocial.Models;

namespace SimpleSocial.Services.AccountServices
{
    public interface IMyProfileServices
    {
        ICollection<Post> GetUserPosts(string userId);
        string GetWallId(string userId);
    }
}
