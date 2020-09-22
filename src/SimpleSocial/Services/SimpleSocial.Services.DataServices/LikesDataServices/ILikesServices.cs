using System.Threading.Tasks;

namespace SimpleSocial.Services.DataServices.LikesDataServices
{
    public interface ILikesServices
    {
        Task Like(int postId, int userId);

        Task UnLike(int postId, int userId);
    }
}
