using System.Security.Claims;
using System.Threading.Tasks;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.Models.Reports;

namespace SimpleSocial.Services.DataServices.ReportsDataServices
{
    public interface IReportsService
    {
        Task AddReport(string authorId,string postId, ReportReason reason);

        ReportViewModel GetReportDetails(string id);

        ReportViewModel GetSubmitReportViewModel(string postId, ClaimsPrincipal user);

        void DeleteReport(string id, ClaimsPrincipal user);
    }
}
