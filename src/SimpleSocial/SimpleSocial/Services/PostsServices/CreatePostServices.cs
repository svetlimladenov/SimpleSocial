using System;
using SimpleSocial.Data;
using SimpleSocial.Models;
using SimpleSocial.ViewModels.Account;

namespace SimpleSocial.Services.PostsServices
{
    public class CreatePostServices : ICreatePostServices
    {
        private readonly ApplicationDbContext db;

        public CreatePostServices(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void CreatePost(MyProfileViewModel viewModel)
        {
            var post = new Post()
            {
                UserId = viewModel.CreatePost.UserId,
                Title = viewModel.CreatePost.Title,
                WallId = viewModel.CreatePost.WallId,
                Content = viewModel.CreatePost.Content,
            };

            
            db.Posts.Add(post);
            db.SaveChanges();
        }
    }
}