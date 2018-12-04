using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleSocial.Data.Models;

namespace SimpleSocial.Web.Services
{
    public interface IMyProfileServices
    {
        ICollection<Post> GetUserPosts(string userId);

        string GetWallId(string userId);
    }
}
