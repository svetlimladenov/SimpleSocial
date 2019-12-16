using System.Threading.Tasks;
using SimpleSocia.Services.Models.Comments;

namespace SimpleSocial.Services.DataServices.CommentsServices
{
    public interface ICommentsServices
    {
        Task CreateCommentAsync(CommentInputModel inputModel);
    }
}
