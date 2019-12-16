using System.Linq;
using Shouldly;
using SimpleSocia.Services.Models.Comments;
using SimpleSocial.Services.DataServices.CommentsServices;
using Xunit;

namespace SimpleSocial.Services.DataServices.Tests.CommentsServicesTests
{
    public class CommentsServicesTests : BaseServiceInitializer
    {
        private ICommentsServices CommentsServices => (ICommentsServices)this.Provider.GetService(typeof(ICommentsServices));

        [Fact]
        public void CreateComment_ShouldWorkFine()
        {
            var comment = new CommentInputModel()
            {
                AuthorId = "test",
                CommentText = "test",
                PostId = "test",
            };
            this.CommentsServices.CreateCommentAsync(comment);
            
            this.Context.Comments.ToList()[0].CommentText.ShouldBe(comment.CommentText);
            
        }
    }
}
