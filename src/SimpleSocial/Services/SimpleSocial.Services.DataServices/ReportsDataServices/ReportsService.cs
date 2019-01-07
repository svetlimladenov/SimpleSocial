using System;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleSocia.Services.Models.Reports;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;

namespace SimpleSocial.Services.DataServices.ReportsDataServices
{
    public class ReportsService : IReportsService
    {
        private readonly IRepository<Post> postRepository;
        private readonly IRepository<PostReport> reportsRepository;

        public ReportsService(IRepository<Post> postRepository,IRepository<PostReport> reportsRepository)
        {
            this.postRepository = postRepository;
            this.reportsRepository = reportsRepository;
        }
        public void AddReport(string authorId, string postId, ReportReason reason)
        {
            var report = new PostReport()
            {
                AuthorId = authorId,
                PostId = postId,
                ReportReason = reason,               
            };

            var post = this.postRepository.All().FirstOrDefault(x => x.Id == postId);
            post?.PostReports.Add(report);
            this.postRepository.SaveChangesAsync().GetAwaiter().GetResult();
        }

        public ReportViewModel GetReportDetails(string id)
        {
            var currentReport = this.reportsRepository.All().Include(x => x.Post).ThenInclude(x => x.User).ThenInclude(u => u.ProfilePicture).Include(x => x.Author).ThenInclude(a => a.ProfilePicture).FirstOrDefault(x => x.Id == id);
            var reportViewModel = Mapper.Map<PostReport, ReportViewModel>(currentReport);
            return reportViewModel;
        }
    }
}
