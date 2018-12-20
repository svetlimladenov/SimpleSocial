using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using SimpleSocial.Data.Models;

namespace SimpleSocial.Services.DataServices.Account
{
    public interface IUserServices
    {
        SimpleSocialUser GetUser(ClaimsPrincipal user);
    }
}
