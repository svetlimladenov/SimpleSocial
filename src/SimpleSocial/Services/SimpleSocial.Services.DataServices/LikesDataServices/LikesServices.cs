using System.Linq;
using System.Threading.Tasks;
using SimpleSocial.Data;
using SimpleSocial.Data.Models;

namespace SimpleSocial.Services.DataServices.LikesDataServices
{
    public class LikesServices : ILikesServices
    {
        private readonly SimpleSocialContext dbContext;

        public LikesServices(SimpleSocialContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Like(int postId, int userId)
        {
            var userLike = new UserLike()
            {
                PostId = postId,
                UserId = userId,
            };

            await this.dbContext.UserLikes.AddAsync(userLike);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task UnLike(int postId, int userId)
        {
            var currentLike = this.dbContext.UserLikes.FirstOrDefault(x => x.UserId == userId && x.PostId == postId);
            if (currentLike == null)
            {
                return;
            }

            this.dbContext.UserLikes.Remove(currentLike);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
