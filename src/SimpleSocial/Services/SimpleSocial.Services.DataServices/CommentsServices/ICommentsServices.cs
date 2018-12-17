using System.Collections.Generic;
using System.Text;
using SimpleSocia.Services.Models.Comments;

namespace SimpleSocial.Services.DataServices.CommentsServices
{
    public interface ICommentsServices
    {
        void CreateComment(CommentInputModel inputModel);
    }
}
