using System.Linq;
using Microsoft.AspNetCore.Identity;
using Shouldly;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.DataServices.FollowersDataServices;
using Xunit;

namespace SimpleSocial.Services.DataServices.Tests.FollowersServicesTests
{
    public class FollowersServicesTests : BaseServiceInitializer
    {
        private IFollowersServices FollowersServices => (IFollowersServices)this.Provider.GetService(typeof(IFollowersServices));
        private UserManager<SimpleSocialUser> UserManager => (UserManager<SimpleSocialUser>)this.Provider.GetService(typeof(UserManager<SimpleSocialUser>));
        private SignInManager<SimpleSocialUser> SingInManager => (SignInManager<SimpleSocialUser>)this.Provider.GetService(typeof(SignInManager<SimpleSocialUser>));

        [Fact]
        public void UserShoudFollowOtherUserSuccessfull()
        {
            var user = new SimpleSocialUser
            {
                Id = "test",
                UserName = "TEST",
                ProfilePictureId = "test"
            };

            var userToFollow = new SimpleSocialUser
            {
                Id = "toFollow",
                UserName = "TEST",
                ProfilePictureId = "test"
            };
            //test must follow userToFollow
            this.UserManager.CreateAsync(user).GetAwaiter();
            this.UserManager.CreateAsync(userToFollow).GetAwaiter();
            var claims = this.SingInManager.CreateUserPrincipalAsync(user).GetAwaiter().GetResult();

            this.FollowersServices.Follow(userToFollow.Id,claims.Identity.Name);

            var currentUserFollowing = this.Context.UserFollowers.ToArray()[0];

            currentUserFollowing.FollowerId.ShouldBe(user.Id);
        }

        [Fact]
        public void GetUserFollowers_ShouldWorkFine()
        {
            var user = new SimpleSocialUser
            {
                Id = "test",
                UserName = "TEST",
                ProfilePictureId = "test"
            };
            var follower = new SimpleSocialUser
            {
                Id = "follower",
                UserName = "TEST",
                ProfilePictureId = "test"
            };
            this.UserManager.CreateAsync(user).GetAwaiter();
            this.UserManager.CreateAsync(follower).GetAwaiter();

            this.Context.UserFollowers.Add(new UserFollower()
            {
                UserId = user.Id,
                Follower = follower,
                FollowerId = "follower"
            });

            this.Context.SaveChanges();
            var claims = this.SingInManager.CreateUserPrincipalAsync(user).GetAwaiter().GetResult();

            var followers = this.FollowersServices.GetFollowers(claims).ToList();
            followers.Count.ShouldBe(1);
        }

        [Fact]
        public void GetUserFollowings_ShouldWorkFine()
        {
            var user = new SimpleSocialUser
            {
                Id = "test",
                UserName = "TEST",
                ProfilePictureId = "test"
            };
            var following = new SimpleSocialUser
            {
                Id = "follower",
                UserName = "TEST",
                ProfilePictureId = "test"
            };
            this.UserManager.CreateAsync(user).GetAwaiter();
            this.UserManager.CreateAsync(following).GetAwaiter();

            this.Context.UserFollowers.Add(new UserFollower()
            {
                User = following,
                UserId = following.Id,
                Follower = user,
                FollowerId = "test"
            });

            this.Context.SaveChanges();
            var claims = this.SingInManager.CreateUserPrincipalAsync(user).GetAwaiter().GetResult();

            var followers = this.FollowersServices.GetFollowings(claims).ToList();
            followers.Count.ShouldBe(1);
        }
    }
}
