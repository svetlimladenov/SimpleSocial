using System;
using System.Collections.Generic;
using System.Text;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.Mapping;

namespace SimpleSocia.Services.Models.Reports
{
    public class ReportsViewModel : IMapFrom<PostReport>
    {
        public ReportReason ReportReason { get; set; }

        public string PostAuthorName { get; set; }

        public string PostAuthorId { get; set; }

        public string GenderText { get; set; }

        public string PostId { get; set; }       
    }
}
