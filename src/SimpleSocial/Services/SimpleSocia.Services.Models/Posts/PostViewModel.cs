using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using SimpleSocia.Services.Models.Account;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.Mapping;

namespace SimpleSocia.Services.Models.Posts
{
    public class PostViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public string UserId { get; set; }

        public string WallId { get; set; }

        public string Title { get; set; }

        public string CharactersCount { get; set; }

        public string Content { get; set; }

        public DateTime CreatedOn { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Post, PostViewModel>().ForMember(x => x.CharactersCount,
                x => x.MapFrom(p => p.Content.Length));
        }
    }
}
