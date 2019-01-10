using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SimpleSocia.Services.Models.Comments;
using SimpleSocia.Services.Models.Followers;
using SimpleSocia.Services.Models.Posts;
using SimpleSocial.Data.Models;

namespace SimpleSocial.Web.Utilities
{
    public class SimpleSocialProfile : Profile
    {
        public SimpleSocialProfile()
        {
            CreateMap<Post, PostViewModel>()
                .ForMember(x => x.CharactersCount, x => x.MapFrom(p => p.Content.Length))
                .ForMember(x => x.Comments, x => x.MapFrom(p => p.Comments))
                .ForMember(x => x.User, x => x.MapFrom(p => p.User))
                .ForMember(x => x.Likes, x => x.MapFrom(p => p.Likes.Select(l => l.User)));

            CreateMap<PostViewModel, Post>();

            CreateMap<CommentInputModel, Comment>();

            CreateMap<SimpleSocialUser, SimpleUserViewModel>();
            
        }
    }
}
