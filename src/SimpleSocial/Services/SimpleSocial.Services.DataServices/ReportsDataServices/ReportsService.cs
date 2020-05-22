using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleSocial.Data;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.FollowersDataServices;
using SimpleSocial.Services.DataServices.PostsServices;
using SimpleSocial.Services.Mapping;
using SimpleSocial.Services.Models.Reports;

namespace SimpleSocial.Services.DataServices.ReportsDataServices
{
    public class ReportsService : IReportsService
    {
        private readonly IMapper mapper;
        private readonly IRepository<Post> postRepository;
        private readonly IRepository<PostReport> reportsRepository;
        private readonly IPostServices postServices;
        private readonly IFollowersServices followersServices;
        private readonly SimpleSocialContext dbContext;
        private readonly UserManager<SimpleSocialUser> userManager;

        public ReportsService(
            IMapper mapper,
            IRepository<Post> postRepository,
            IRepository<PostReport> reportsRepository,
            IPostServices postServices,
            IFollowersServices followersServices,
            SimpleSocialContext dbContext,
            UserManager<SimpleSocialUser> userManager)
        {
            this.mapper = mapper;
            this.postRepository = postRepository;
            this.reportsRepository = reportsRepository;
            this.postServices = postServices;
            this.followersServices = followersServices;
            this.dbContext = dbContext;
            this.userManager = userManager;
        }
        public async Task AddReport(string authorId, string postId, ReportReason reason)
        {
            var report = new PostReport()
            {
                AuthorId = authorId,
                PostId = postId,
                ReportReason = reason,
            };

            await this.dbContext.PostReports.AddAsync(report);
            await this.dbContext.SaveChangesAsync();
        }

        public ReportViewModel GetReportDetails(string id)
        {
            var currentReport = this.dbContext.PostReports.Where(x => x.Id == id).To<ReportViewModel>().FirstOrDefault();
            if (currentReport == null)
            {
                //TODO: Validation Errors
                return null;
            }
            return currentReport;
        }

        public ReportViewModel GetSubmitReportViewModel(string postId, ClaimsPrincipal user)
        {
            var postAuthor = postServices.GetPostAuthor(postId);
            if (postAuthor == null)
            {
                return null;
            }

            //TODO: Get Gender Text From somewhere else
            var genderText = "Him";
            var currentUserId = userManager.GetUserId(user);
            if (postAuthor.Gender == Gender.Male)
            {
                genderText = "Him";
            }
            else if (postAuthor.Gender == Gender.Famale)
            {
                genderText = "Her";
            }
            var isBeingFollowedByCurrentUser = this.followersServices.IsBeingFollowedBy(postAuthor.Id, currentUserId) || postAuthor.Id == currentUserId;
            var viewModel = new ReportViewModel
            {
                PostId = postId,
                ReportReason = new ReportReason(),
                PostAuthorName = postAuthor.UserName,
                PostAuthorId = postAuthor.Id,
                GenderText = genderText,
                IsBeingFollowedByCurrentUser = isBeingFollowedByCurrentUser
            };

            return viewModel;
        }

        public void DeleteReport(string id, ClaimsPrincipal user)
        {
            var currentUser = this.userManager.GetUserAsync(user).GetAwaiter().GetResult();
            var currentUserIsAdmin = this.userManager.IsInRoleAsync(currentUser, "Admin").GetAwaiter().GetResult();
            if (!currentUserIsAdmin)
            {
                return;
            }

            var report = this.reportsRepository.All().FirstOrDefault(x => x.Id == id);
            if (report == null)
            {
                return;
            }

            this.reportsRepository.Delete(report);
            this.reportsRepository.SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}
