using System.Collections.Generic;
using System.Text;

namespace SimpleSocial.Services.DataServices.LikesDataServices
{
    public interface ILikesServices
    {
        void Like(string postId, string userId);

        void UnLike(string postId, string userId);
    }
}
