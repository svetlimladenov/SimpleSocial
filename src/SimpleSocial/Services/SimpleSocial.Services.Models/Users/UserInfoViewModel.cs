using System;
using AutoMapper;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.Mapping;

namespace SimpleSocial.Services.Models.Users
{
    public class UserInfoViewModel : IMapFrom<SimpleSocialUser>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string ProfilePictureURL { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserId { get; set; }

        public string WallId { get; set; }

        public string Description { get; set; }

        public int FollowersCount { get; set; }

        public int FollowingsCount { get; set; }

        public bool IsBeingFollowedByCurrentUser { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public DateTime? BirthDay { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Age { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<SimpleSocialUser, UserInfoViewModel>().ForMember(x => x.Age, x => x.MapFrom(
                u => u.BirthDay.GetAge()
            ));

            configuration.CreateMap<SimpleSocialUser, UserInfoViewModel>().ForMember(x => x.UserId, x => x.MapFrom(
                u => u.Id
            ));
        }
    }
}
