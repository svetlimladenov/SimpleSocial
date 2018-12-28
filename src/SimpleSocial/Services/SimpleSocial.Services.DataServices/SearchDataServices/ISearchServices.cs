using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using SimpleSocia.Services.Models.Search;

namespace SimpleSocial.Services.DataServices.SearchDataServices
{
    public interface ISearchServices
    {
        SearchResultsViewModel GetResultOfSearch(string searchText,ClaimsPrincipal currentUser);
    }
}
