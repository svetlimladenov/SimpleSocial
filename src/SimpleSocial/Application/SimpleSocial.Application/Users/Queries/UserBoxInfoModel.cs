using System;
namespace SimpleSocial.Application.Users.Queries
{
    public class UserBoxInfoModel
    {
        public string Username { get; set; }

        public string FullName { get; set; }

        public int? Age { get; set; }

        public DateTime? Birthday { get; set; }

        public DateTime JoinedOn { get; set; }

        public string Location { get; set; }

        public string ProfilePictureUrl { get; set; }
    }
}
