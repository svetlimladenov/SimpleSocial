using SimpleSocial.Services.Models.Followers;

namespace SimpleSocial.Services.Models.Search
{
    public class SearchResultsViewModel
    {
        public string SearchText { get; set; }

        public UsersListViewModel UsersFound {get; set; }
    }
}