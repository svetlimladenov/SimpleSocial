using System;
using System.Linq;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;

namespace SimpleSocial.Services.DataServices.ReportsDataServices
{
    public class ReportsService : IReportsService
    {
        private readonly IRepository<Post> postRepository;

        public ReportsService(IRepository<Post> postRepository)
        {
            this.postRepository = postRepository;
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
    }
}
