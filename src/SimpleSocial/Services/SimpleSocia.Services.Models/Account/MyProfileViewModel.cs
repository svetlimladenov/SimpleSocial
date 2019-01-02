using SimpleSocia.Services.Models.Followers;

namespace SimpleSocia.Services.Models.Account
{
    public class MyProfileViewModel : PostsFeedAndUserInfoViewModel
    {
        public UsersListViewModel WhoToFollow { get; set; }

        public bool IsValidProfilePicture { get; set; } = true;
    }
}
