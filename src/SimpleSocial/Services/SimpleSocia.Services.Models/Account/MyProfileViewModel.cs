namespace SimpleSocia.Services.Models.Account
{
    public class MyProfileViewModel : UserProfileViewModel
    {
        public CreatePostInputModel CreatePost { get; set; }

        public bool IsValidProfilePicture { get; set; } = true;
    }
}
