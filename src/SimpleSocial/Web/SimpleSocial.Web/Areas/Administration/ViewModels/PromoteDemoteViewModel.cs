using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleSocia.Services.Models.Followers;

namespace SimpleSocial.Web.Areas.Administration.ViewModels
{
    public class PromoteDemoteViewModel
    {
        public ICollection<SimpleUserViewModel> NonAdminUsers { get; set; }

        public ICollection<SimpleUserViewModel> AdminUsers { get; set; }  
    }
}
