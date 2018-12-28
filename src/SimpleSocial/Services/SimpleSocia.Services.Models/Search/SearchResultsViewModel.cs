using System.Collections.Generic;
using SimpleSocia.Services.Models.Followers;

namespace SimpleSocia.Services.Models.Search
{
    public class SearchResultsViewModel
    {
        public string SearchText { get; set; }

        public UsersListViewModel UsersFound {get; set; }
    }
}