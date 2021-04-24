using System.Collections.Generic;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.Models.Reports;

namespace SimpleSocial.Web.Areas.Administration.ViewModels
{
    public class AllReportsViewModel
    {
        public int PagesCount { get; set; }

        public int CurrentPageNumber { get; set; }

        public MinifiedPostsListViewModel PostReports { get; set; }    
    }
}
