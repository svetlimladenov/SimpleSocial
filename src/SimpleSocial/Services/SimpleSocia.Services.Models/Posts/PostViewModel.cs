using System;
using System.Collections.Generic;
using AutoMapper;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.Mapping;

namespace SimpleSocia.Services.Models.Posts
{
    public class PostViewModel : IMapFrom<Post>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public SimpleSocialUser User { get; set; }

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

            configuration.CreateMap<Post, PostViewModel>().ForMember(x => x.Comments, x => x.MapFrom(p => p.Comments));

            configuration.CreateMap<Post, PostViewModel>().ForMember(x => x.User, x => x.MapFrom(p => p.User));
        }
    }
}
