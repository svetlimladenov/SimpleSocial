using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleSocia.Services.Models.Followers;

namespace SimpleSocial.Web.Components
{
    [ViewComponent(Name = "ListOfFollowers")]
    public class ListOfFollowersViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(UsersListViewModel viewModel)
        {
            if (viewModel == null)
            {
                viewModel = new UsersListViewModel();
            }

            return View(viewModel);
        }
    }
}
