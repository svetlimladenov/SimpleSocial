using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SimpleSocial.Services.Models.Account
{
    public class UploadProfilePictureInputModel
    {
        [DataType(DataType.Upload)]
        public IFormFile UploadImage { get; set; }
    }
}
