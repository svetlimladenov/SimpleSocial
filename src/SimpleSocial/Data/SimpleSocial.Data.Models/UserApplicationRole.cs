using Microsoft.AspNetCore.Identity;

namespace SimpleSocial.Data.Models
{
    public class UserApplicationRole : IdentityUserRole<int>
    {
        public bool IsActive { get; set; }
    }
}
