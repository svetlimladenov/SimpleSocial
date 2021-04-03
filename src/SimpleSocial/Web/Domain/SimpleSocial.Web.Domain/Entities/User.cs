using System;

namespace SimpleSocial.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfilePictureURL { get; set; }

        public DateTime? BirthDay { get; set; }

        public string Description { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
