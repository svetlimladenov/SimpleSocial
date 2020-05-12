using System;
using AutoMapper;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.Mapping;

namespace SimpleSocial.Services.Models.Reports
{
    public class ReportViewModel : IMapFrom<PostReport>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public ReportReason ReportReason { get; set; }

        public string AuthorId { get; set; }

        public SimpleSocialUser Author { get; set; }

        public string PostAuthorName { get; set; }

        public string PostAuthorId { get; set; }

        public string GenderText { get; set; }

        public string PostId { get; set; }

        public Post Post { get; set; }

        public bool IsBeingFollowedByCurrentUser { get; set; }

        public DateTime ReportedOn { get; set; }
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<PostReport, ReportViewModel>()
                .ForMember(x => x.PostAuthorName, x => x.MapFrom(pr => pr.Post.User.UserName));

            configuration.CreateMap<PostReport, ReportViewModel>()
                .ForMember(x => x.PostAuthorId, x => x.MapFrom(pr => pr.Post.User.Id));
        }
    }
}
