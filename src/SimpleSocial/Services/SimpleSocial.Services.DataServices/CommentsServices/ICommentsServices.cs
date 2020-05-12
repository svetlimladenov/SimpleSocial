using System.Threading.Tasks;
using SimpleSocial.Services.Models.Comments;

namespace SimpleSocial.Services.DataServices.CommentsServices
{
    public interface ICommentsServices
    {
        Task CreateCommentAsync(CommentInputModel inputModel);
    }
}
