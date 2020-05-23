using System.Threading.Tasks;

namespace SimpleSocial.Services.DataServices.LikesDataServices
{
    public interface ILikesServices
    {
        Task Like(string postId, string userId);

        Task UnLike(string postId, string userId);
    }
}
