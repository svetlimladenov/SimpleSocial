using System.Collections.Generic;
using SimpleSocial.Services.Models.Followers;

namespace SimpleSocial.Web.Areas.Administration.ViewModels
{
    public class PromoteDemoteViewModel
    {
        public ICollection<SimpleUserViewModel> NonAdminUsers { get; set; }

        public ICollection<SimpleUserViewModel> AdminUsers { get; set; }

        public ICollection<string> AllUsers { get; set; }
    }
}
    