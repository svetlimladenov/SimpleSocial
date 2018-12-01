namespace SimpleSocial.ViewModels.Account
{
    public class CreatePostInputModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public string WallId { get; set; }

        public int Likes { get; set; }
    }
}