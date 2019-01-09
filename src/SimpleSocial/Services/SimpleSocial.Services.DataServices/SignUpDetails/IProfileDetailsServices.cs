using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using SimpleSocia.Services.Models.SignUp;

namespace SimpleSocial.Services.DataServices.SignUpDetails
{
    public interface IProfileDetailsServices
    {
        List<string> GetCounties();
    }
}
