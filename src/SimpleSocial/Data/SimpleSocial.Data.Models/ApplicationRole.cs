using Microsoft.AspNetCore.Identity;
using System;

namespace SimpleSocial.Data.Models
{
    public class ApplicationRole : IdentityRole<int>
    {
        public ApplicationRole()
            :base()
        {
        }

        public ApplicationRole(string name)
            :base(name)
        {
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
