using AutoMapper;
using SimpleSocia.Services.Models.Comments;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;

namespace SimpleSocial.Services.DataServices.CommentsServices
{
    public class CommentsServices : ICommentsServices
    {
        private readonly IRepository<Comment> commentsRepository;

        public CommentsServices(IRepository<Comment> commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }

        public void CreateComment(CommentInputModel inputModel)
        {
            var comment = Mapper.Map<Comment>(inputModel);

            this.commentsRepository.AddAsync(comment).GetAwaiter().GetResult();
            this.commentsRepository.SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}