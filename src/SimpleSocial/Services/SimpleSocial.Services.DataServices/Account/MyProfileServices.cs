﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using SimpleSocia.Services.Models.Posts;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.Mapping;


namespace SimpleSocial.Services.DataServices.Account
{
    public class MyProfileServices : IMyProfileServices
    {
        private readonly IRepository<Post> postRepository;
        private readonly IRepository<Wall> wallRepository;
        private readonly IRepository<ProfilePicture> profilePicturesRepository;
        private readonly UserManager<SimpleSocialUser> userManager;
        private readonly IHostingEnvironment hostingEnvironment;

        public MyProfileServices(
            IRepository<Post> postRepository,
            IRepository<Wall> wallRepository,
            IRepository<ProfilePicture> profilePicturesRepository,
            UserManager<SimpleSocialUser> userManager,
            IHostingEnvironment hostingEnvironment
            )
        {
            this.postRepository = postRepository;
            this.wallRepository = wallRepository;
            this.profilePicturesRepository = profilePicturesRepository;
            this.userManager = userManager;
            this.hostingEnvironment = hostingEnvironment;
        }

        public IEnumerable<PostViewModel> GetUserPosts(ClaimsPrincipal user)
        {
            var userId = userManager.GetUserId(user);
            var posts = this.postRepository.All().Where(x => x.UserId == userId).To<PostViewModel>().ToList().OrderByDescending(x => x.CreatedOn);

            return posts;
        }

        public string GetWallId(ClaimsPrincipal user)
        {
            var userId = userManager.GetUserId(user);
            var posts = wallRepository.All().FirstOrDefault(w => w.UserId == userId)?.Id;

            return posts;
        }

        public ProfilePicture GetProfilePicture(ClaimsPrincipal user)
        {
            var userId = userManager.GetUserId(user);
            var profilePicture = profilePicturesRepository.All().FirstOrDefault(x => x.UserId == userId);
            var wwwRootPath = hostingEnvironment.WebRootPath;
            if (File.Exists(wwwRootPath + $"/profile-pictures/{profilePicture?.FileName}"))
            {
                return profilePicture;
            }
            
            return new ProfilePicture
            {
                FileName = "default.jpg",
                UserId = userId,
            };
        }
    }
}