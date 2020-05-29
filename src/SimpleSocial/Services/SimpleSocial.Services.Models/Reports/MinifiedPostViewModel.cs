using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleSocial.Services.Models.Reports
{
    public class MinifiedPostViewModel
    {
        public string Id { get; set; }

        public string PostAuthorId { get; set; }

        public string PostAuthorUsername { get; set; }

        public string ReportAuthorId { get; set; }

        public string ReportAuthorUsername { get; set; }

        public string ReportReason { get; set; }

        public int MyProperty { get; set; }
    }
}
