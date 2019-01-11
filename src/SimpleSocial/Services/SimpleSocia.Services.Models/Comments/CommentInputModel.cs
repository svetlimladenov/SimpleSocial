using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.Mapping;

namespace SimpleSocia.Services.Models.Comments
{
    public class CommentInputModel : IMapTo<Comment>, IValidatableObject
    {
        [Required]
        public string PostId { get; set; }

        public Post Post { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public SimpleSocialUser Author { get; set; }

        [Required]
        public string CommentText { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var postRepository = (IRepository<Post>)validationContext
                .GetService(typeof(IRepository<Post>));

            var userRepository = (IRepository<SimpleSocialUser>)validationContext
                .GetService(typeof(IRepository<SimpleSocialUser>));

            if (!postRepository.All().Any(x => x.Id == this.PostId))
            {
                yield return new ValidationResult("Invalid post id");
            }

            if (!userRepository.All().Any(x => x.Id == this.AuthorId))
            {
                yield return new ValidationResult("Invalid author id");
            }
        }
    }
}