using CloudinaryDotNet;
using Microsoft.AspNetCore.Identity;
using SimpleSocial.Data;
using SimpleSocial.Data.Models;
using System.Linq;
using System.Security.Claims;
using SimpleSocial.Services.Models.Account;
using System.Threading.Tasks;

namespace SimpleSocial.Services.DataServices.ProfilePictureServices
{
    public class ProfilePictureService : IProfilePictureService
    {
        private readonly SimpleSocialContext dbContext;
        private readonly UserManager<SimpleSocialUser> userManager;

        public ProfilePictureService(
            SimpleSocialContext dbContext,
            UserManager<SimpleSocialUser> userManager
            )
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task UploadProfilePictureCloudinary(ClaimsPrincipal user, UploadProfilePictureInputModel inputModel)
        {
            var userId = this.userManager.GetUserId(user);

            CloudinaryDotNet.Account account =
                new CloudinaryDotNet.Account("svetlinmld", "412472163518427", "M90sSSvXSYNzKQ3-l7qb-KGLpSY");

            CloudinaryDotNet.Cloudinary cloudinary = new CloudinaryDotNet.Cloudinary(account);

            var fileName = $"{userId}_Profile_Picture";

            var stream = inputModel.UploadImage.OpenReadStream();

            CloudinaryDotNet.Actions.ImageUploadParams uploadParams = new CloudinaryDotNet.Actions.ImageUploadParams()
            {
                File = new FileDescription(inputModel.UploadImage.FileName, stream),
                PublicId = fileName,
            };

            CloudinaryDotNet.Actions.ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);

            var updatedUrl = cloudinary.GetResource(uploadResult.PublicId).Url;

            await SaveImageNameToDb(user, updatedUrl);
        }

        private async Task SaveImageNameToDb(ClaimsPrincipal user, string imagePath)
        {
            var userId = userManager.GetUserId(user);
            var currentUser = this.dbContext.Users.FirstOrDefault(x => x.Id == userId);
            currentUser.ProfilePictureURL = imagePath;
            await this.dbContext.SaveChangesAsync();
        }

        public string GetUserProfilePictureURL(string userId)
            => this.dbContext
                   .Users.FirstOrDefault(x => x.Id == userId).ProfilePictureURL;
    }
}
