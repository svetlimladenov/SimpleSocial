using System.Collections.Generic;
using SimpleSocial.Data.Models;

namespace SimpleSocial.Web.Areas.Administration.ViewModels
{
    public class AllReportsViewModel
    {
        public IEnumerable<PostReport> PostReports { get; set; }    
    }
}
