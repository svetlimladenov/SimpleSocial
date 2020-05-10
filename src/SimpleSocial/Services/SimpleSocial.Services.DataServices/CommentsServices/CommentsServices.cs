using System.Threading.Tasks;
using AutoMapper;
using SimpleSocia.Services.Models.Comments;
using SimpleSocial.Data;
using SimpleSocial.Data.Models;

namespace SimpleSocial.Services.DataServices.CommentsServices
{
    public class CommentsServices : ICommentsServices
    {
        private readonly SimpleSocialContext dbContext;
        private readonly IMapper mapper;

        public CommentsServices(SimpleSocialContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task CreateCommentAsync(CommentInputModel inputModel)
        {
            var comment = mapper.Map<Comment>(inputModel);

            await this.dbContext.AddAsync(comment);
            await this.dbContext.SaveChangesAsync();
        }
    }
}