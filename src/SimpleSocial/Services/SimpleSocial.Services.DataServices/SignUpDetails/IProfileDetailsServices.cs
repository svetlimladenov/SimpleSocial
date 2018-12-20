using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using SimpleSocia.Services.Models.SignUp;

namespace SimpleSocial.Services.DataServices.SignUpDetails
{
    public interface IProfileDetailsServices
    {
        void SetNames(ProfileDetailsInputModel inputModel, ClaimsPrincipal user);

        void SetBirthDay(ProfileDetailsInputModel inputModel, ClaimsPrincipal user);

        void SetLivingPlace(ProfileDetailsInputModel inputModel, ClaimsPrincipal user);

        List<string> GetCounties();
    }
}
