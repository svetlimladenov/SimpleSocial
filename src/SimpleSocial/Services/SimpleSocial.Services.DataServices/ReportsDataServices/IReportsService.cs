using System.Security.Claims;
using System.Threading.Tasks;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.Models.Reports;

namespace SimpleSocial.Services.DataServices.ReportsDataServices
{
    public interface IReportsService
    {
        Task AddReport(int authorId, int postId, ReportReason reason);

        ReportViewModel GetReportDetails(int id);

        ReportViewModel GetSubmitReportViewModel(int postId, ClaimsPrincipal user);

        Task DeleteReport(int id, ClaimsPrincipal user);

        Task<int> GetReportsCount();
    }
}
