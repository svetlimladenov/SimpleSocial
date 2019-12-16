﻿using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using SimpleSocia.Services.Models.Account;
using SimpleSocia.Services.Models.Posts;
using SimpleSocial.Data.Models;

namespace SimpleSocial.Services.DataServices.PostsServices
{
    public interface IPostServices
    {
        Task CreatePost(MyProfileViewModel viewModel);

        ICollection<PostViewModel> GetUserPosts(string userId, string currrentUserId, int pageNumber);

        PostViewModel GetPostById(string id);

        SinglePostViewComponentModel GetSinglePostViewComponentModel(string id, string visitorId);

        ICollection<PostViewModel> GetNewsFeedPosts(string currrentUserId, int pageNumber);

        SimpleSocialUser GetPostAuthor(string postId);

        void DeletePost(string id, ClaimsPrincipal user);

        bool PostExists(string id);
    }
}
