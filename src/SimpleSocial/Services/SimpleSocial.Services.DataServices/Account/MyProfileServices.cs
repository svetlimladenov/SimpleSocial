using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using AutoMapper;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SimpleSocia.Services.Models.Account;
using SimpleSocia.Services.Models.Posts;
using SimpleSocia.Services.Models.Users;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;


namespace SimpleSocial.Services.DataServices.Account
{
    public class MyProfileServices : IMyProfileServices
    {
        private readonly IRepository<Wall> wallRepository;
        private readonly IRepository<ProfilePicture> profilePicturesRepository;
        private readonly UserManager<SimpleSocialUser> userManager;
        private readonly IRepository<SimpleSocialUser> userRepository;

        public MyProfileServices(
            IRepository<Wall> wallRepository,
            IRepository<ProfilePicture> profilePicturesRepository,
            UserManager<SimpleSocialUser> userManager,
            IRepository<SimpleSocialUser> userRepository
            )
        {
            this.wallRepository = wallRepository;
            this.profilePicturesRepository = profilePicturesRepository;
            this.userManager = userManager;
            this.userRepository = userRepository;
        }

        public ProfilePicture GetUserProfilePicture(string userId)
        {
            var profilePicture = profilePicturesRepository.All().FirstOrDefault(x => x.UserId == userId);
            return profilePicture;
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
            var currentProfilePicture = profilePicturesRepository.All().FirstOrDefault(x => x.UserId == userId);
            while (currentProfilePicture != null)
            {
                profilePicturesRepository.Delete(currentProfilePicture);
                profilePicturesRepository.SaveChangesAsync().GetAwaiter().GetResult();
                currentProfilePicture = profilePicturesRepository.All().FirstOrDefault(x => x.UserId == userId);
            }

            var newProfilePic = new ProfilePicture
            {
                FileName = imagePath,
                UserId = userId,
            };

            profilePicturesRepository.AddAsync(newProfilePic).GetAwaiter().GetResult();

            var userProfilePicToChange = userManager.GetUserAsync(user).GetAwaiter().GetResult();

            profilePicturesRepository.SaveChangesAsync().GetAwaiter().GetResult();

            userProfilePicToChange.ProfilePictureId = newProfilePic.Id;

            userRepository.SaveChangesAsync().GetAwaiter().GetResult();
        }

        public UserInfoViewModel GetUserInfo(string userId)
        {
            var user = this.userRepository.All().FirstOrDefault(x => x.Id == userId);
            if (user == null)
            {
                return null;
            }

            var userInfo = new UserInfoViewModel
            {
                UserId = user.Id,
                UserName = user.UserName,
                ProfilePicture = this.GetUserProfilePicture(userId),
                WallId = user.WallId,
            };

            return userInfo;
        }
    }
}
