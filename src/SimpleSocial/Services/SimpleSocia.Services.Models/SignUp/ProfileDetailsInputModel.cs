using System;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.Mapping;

namespace SimpleSocia.Services.Models.SignUp
{
    public class ProfileDetailsInputModel : IMapFrom<SimpleSocialUser>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDay { get; set; }

        public Gender Gender { get; set; }

        public string Description { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
    }
}
