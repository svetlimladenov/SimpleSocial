using System.Security.Claims;
using SimpleSocia.Services.Models.Reports;
using SimpleSocial.Data.Models;

namespace SimpleSocial.Services.DataServices.ReportsDataServices
{
    public interface IReportsService
    {
        void AddReport(string authorId,string postId, ReportReason reason);

        ReportViewModel GetReportDetails(string id);

        ReportViewModel GetSubmitReportViewModel(string postId, ClaimsPrincipal user);

        void DeleteReport(string id, ClaimsPrincipal user);
    }
}
