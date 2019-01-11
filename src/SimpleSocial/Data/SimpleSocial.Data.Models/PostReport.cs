using System;

namespace SimpleSocial.Data.Models
{
    public class PostReport
    {
        public string Id { get; set; }

        public string AuthorId { get; set; }

        public SimpleSocialUser Author { get; set; }

        public string PostId { get; set; }

        public Post Post { get; set; }

        public ReportReason ReportReason { get; set; }

        public DateTime ReportedOn { get; set; } = DateTime.UtcNow;
    }
}
