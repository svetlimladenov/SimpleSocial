using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleSocial.Data.Models;

namespace SimpleSocial.Web.Areas.Administration.ViewModels
{
    public class AllReportsViewModel
    {
        public IEnumerable<PostReport> PostReports { get; set; }    
    }
}
