using System.ComponentModel.DataAnnotations;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.Mapping;

namespace SimpleSocial.Services.Models.Comments
{
    public class CommentInputModel : IMapTo<Comment>
    {
        [Required]
        public string PostId { get; set; }

        public Post Post { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public SimpleSocialUser Author { get; set; }

        [Required]
        public string CommentText { get; set; }

        //TODO: Add Validations which were removed from the IValidatable Object
    }
}