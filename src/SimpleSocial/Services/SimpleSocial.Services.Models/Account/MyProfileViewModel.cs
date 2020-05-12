using SimpleSocial.Services.Models.Followers;

namespace SimpleSocial.Services.Models.Account
{
    public class MyProfileViewModel : PostsFeedAndUserInfoViewModel
    {
        public UsersListViewModel WhoToFollow { get; set; }

        public bool IsValidProfilePicture { get; set; } = true;
    }
}
