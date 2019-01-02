using System;
using System.Collections.Generic;
using System.Text;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.Mapping;

namespace SimpleSocia.Services.Models.Users
{
    public class UserInfoViewModel : IMapFrom<SimpleSocialUser>
    {
        public ProfilePicture ProfilePicture { get; set; }

        public string UserName { get; set; }

        public string UserId { get; set; }

        public string WallId { get; set; }

        public string Description { get; set; }
    }
}
