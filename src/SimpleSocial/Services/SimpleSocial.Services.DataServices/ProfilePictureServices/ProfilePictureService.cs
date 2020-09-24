using CloudinaryDotNet;
using Microsoft.AspNetCore.Identity;
using SimpleSocial.Data;
using SimpleSocial.Data.Models;
using System.Linq;
using System.Security.Claims;
using SimpleSocial.Services.Models.Account;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using SimpleSocial.Services.DataServices.UsersDataServices;

namespace SimpleSocial.Services.DataServices.ProfilePictureServices
{
    public class ProfilePictureService : IProfilePictureService
    {
        private readonly SimpleSocialContext dbContext;
        private readonly IConfiguration configuration;
        private readonly IUserServices userServices;

        public ProfilePictureService(
            SimpleSocialContext dbContext,
            IConfiguration configuration,
            IUserServices userServices
            )
        {
            this.dbContext = dbContext;
            this.configuration = configuration;
            this.userServices = userServices;
        }

        public bool VerifyPicture(UploadProfilePictureInputModel pictureModel)
        {
            if (pictureModel.UploadImage == null)
            {
                //TODO: Validation errors
                return false;
            }
            var indexOfImgExtensionDot = pictureModel.UploadImage.FileName.LastIndexOf('.');

            var imgExtension = pictureModel.UploadImage.FileName.Substring(indexOfImgExtensionDot).ToLower();

            string[] allowedExtensions = { ".jpg", ".jpeg", ".png" };

            return allowedExtensions.Contains(imgExtension);
        }

        public async Task UploadProfilePictureCloudinary(ClaimsPrincipal user, UploadProfilePictureInputModel inputModel)
        {
            var userId = this.userServices.GetUserId(user);
            var coudinaryUsername = configuration.GetValue<string>("Cloudinary:Username");
            var apiKey = configuration.GetValue<string>("Cloudinary:ApiKey");
            var apiSecret = configuration.GetValue<string>("Cloudinary:ApiSecret");
            CloudinaryDotNet.Account account =
                new CloudinaryDotNet.Account(coudinaryUsername, apiKey, apiSecret);

            Cloudinary cloudinary = new Cloudinary(account);

            var fileName = $"{userId}_Profile_Picture";

            var stream = inputModel.UploadImage.OpenReadStream();

            CloudinaryDotNet.Actions.ImageUploadParams uploadParams = new CloudinaryDotNet.Actions.ImageUploadParams()
            {
                File = new FileDescription(inputModel.UploadImage.FileName, stream),
                PublicId = fileName,
            };

            CloudinaryDotNet.Actions.ImageUploadResult uploadResult = await cloudinary.UploadAsync(uploadParams);

            var updatedUrl = (await cloudinary.GetResourceAsync(uploadResult.PublicId)).Url;

            await SaveImageNameToDb(userId, updatedUrl);
        }

        private async Task SaveImageNameToDb(int userId, string imagePath)
        {
            var currentUser = await this.dbContext.Users.FindAsync(userId);
            currentUser.ProfilePictureURL = imagePath;
            await this.dbContext.SaveChangesAsync();
        }
    }
}
