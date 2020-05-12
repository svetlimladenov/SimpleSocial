using CloudinaryDotNet;
using Microsoft.AspNetCore.Identity;
using SimpleSocial.Data;
using SimpleSocial.Data.Models;
using System.Linq;
using System.Security.Claims;
using SimpleSocial.Services.Models.Account;

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

        public void UploadProfilePictureCloudinary(ClaimsPrincipal user, UploadProfilePictureInputModel inputModel)
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

            SaveImageNameToDb(user, updatedUrl);
        }

        private void SaveImageNameToDb(ClaimsPrincipal user, string imagePath)
        {
            var userId = userManager.GetUserId(user);
            var currentProfilePicture = this.dbContext.ProfilePictures.FirstOrDefault(x => x.UserId == userId);
            while (currentProfilePicture != null)
            {
                this.dbContext.ProfilePictures.Remove(currentProfilePicture);
                this.dbContext.SaveChangesAsync().GetAwaiter().GetResult();
                currentProfilePicture = this.dbContext.ProfilePictures.FirstOrDefault(x => x.UserId == userId);
            }

            var newProfilePic = new ProfilePicture
            {
                FileName = imagePath,
                UserId = userId,
            };

            //TODO: await it 
            this.dbContext.AddAsync(newProfilePic).GetAwaiter().GetResult();

            var userProfilePicToChange = userManager.GetUserAsync(user).GetAwaiter().GetResult();

            this.dbContext.SaveChangesAsync().GetAwaiter().GetResult();

            userProfilePicToChange.ProfilePictureId = newProfilePic.Id;

            this.dbContext.SaveChangesAsync().GetAwaiter().GetResult();
        }

        public string GetUserProfilePictureURL(string userId)
            => this.dbContext
                   .ProfilePictures.Where(x => x.UserId == userId).Select(x => x.FileName).FirstOrDefault();

    }
}
