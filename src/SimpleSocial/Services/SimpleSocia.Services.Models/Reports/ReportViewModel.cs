using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.Mapping;

namespace SimpleSocia.Services.Models.Reports
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
        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<PostReport, ReportViewModel>()
                .ForMember(x => x.PostAuthorName, x => x.MapFrom(pr => pr.Post.User.UserName));

            configuration.CreateMap<PostReport, ReportViewModel>()
                .ForMember(x => x.PostAuthorId, x => x.MapFrom(pr => pr.Post.User.Id));
        }
    }
}
