using System.Collections.Generic;
using System.Security.Claims;
using SimpleSocial.Services.Models.Followers;
using SimpleSocial.Data.Models;
using System.Threading.Tasks;
using SimpleSocial.Services.Models.Reports;

namespace SimpleSocial.Web.Areas.Administration.Services
{
    public interface IAdministrationServices
    {
        ICollection<SimpleUserViewModel> GetAdminUsers(ClaimsPrincipal currentUser, List<SimpleUserViewModel> users = null);

        ICollection<SimpleUserViewModel> GetNonAdminUsers(ClaimsPrincipal currentUser, List<SimpleUserViewModel> users = null);

        Task PromoteUser(string id);

        Task DemoteUser(string id);

        string GetRandomQuote();

        Task<MinifiedPostsListViewModel> GetAllReports(int pageNumber, int numberOfPosts = 10);
    }
}
