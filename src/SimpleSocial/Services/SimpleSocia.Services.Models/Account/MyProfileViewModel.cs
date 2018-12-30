using SimpleSocia.Services.Models.Followers;

namespace SimpleSocia.Services.Models.Account
{
    public class MyProfileViewModel : UserProfileViewModel
    {
        public UsersListViewModel WhoToFollow { get; set; }

        public CreatePostInputModel CreatePost { get; set; }

        public bool IsValidProfilePicture { get; set; } = true;
    }
}
