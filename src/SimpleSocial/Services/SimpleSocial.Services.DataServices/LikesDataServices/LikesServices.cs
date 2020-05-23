using System.Linq;
using System.Threading.Tasks;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;

namespace SimpleSocial.Services.DataServices.LikesDataServices
{
    public class LikesServices : ILikesServices
    {
        private readonly IRepository<UserLike> userLikesRepository;
        private readonly IRepository<SimpleSocialUser> userRepository;

        public LikesServices(
            IRepository<UserLike> userLikesRepository,
            IRepository<SimpleSocialUser> userRepository)
        {
            this.userLikesRepository = userLikesRepository;
            this.userRepository = userRepository;
        }
        public async Task Like(string postId, string userId)
        {
            var userLike = new UserLike()
            {
                PostId = postId,
                UserId = userId,
            };
            var user = this.userRepository.All().FirstOrDefault(x => x.Id == userId);
            if (user == null)
            {
                return;
            }

            user.Likes.Add(userLike);
            this.userLikesRepository.AddAsync(userLike).GetAwaiter().GetResult();

            //save changes
            await this.userRepository.SaveChangesAsync();
            await this.userLikesRepository.SaveChangesAsync();
        }

        public async Task UnLike(string postId, string userId)
        {
            var user = this.userRepository.All().FirstOrDefault(x => x.Id == userId);
            if (user == null)
            {
                return;
            }

            var currentLike = this.userLikesRepository.All()
                .FirstOrDefault(x => x.UserId == userId && x.PostId == postId);
            if (currentLike == null)
            {
                return;
            }

            this.userLikesRepository.Delete(currentLike);
            user.Likes.ToList().RemoveAll(x => x.PostId == postId && x.UserId == userId);

            await this.userLikesRepository.SaveChangesAsync();
            await this.userRepository.SaveChangesAsync();
        }
    }
}
