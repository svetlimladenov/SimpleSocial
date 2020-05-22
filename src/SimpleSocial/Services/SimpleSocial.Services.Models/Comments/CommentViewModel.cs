using SimpleSocial.Data.Models;
using SimpleSocial.Services.Mapping;
using System;

namespace SimpleSocial.Services.Models.Comments
{
    public class CommentViewModel : IMapFrom<Comment>
    {
        public string Id { get; set; }

        public string AuthorId { get; set; }

        public CommentAuthorViewModel Author { get; set; }

        public DateTime PostedOn { get; set; }

        public string CommentText { get; set; }
    }
}
