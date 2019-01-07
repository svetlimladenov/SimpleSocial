using System.Collections.Generic;
using System.Text;
using SimpleSocia.Services.Models.Reports;
using SimpleSocial.Data.Models;

namespace SimpleSocial.Services.DataServices.ReportsDataServices
{
    public interface IReportsService
    {
        void AddReport(string authorId,string postId, ReportReason reason);
        ReportViewModel GetReportDetails(string id);
    }
}
