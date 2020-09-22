using System;

namespace SimpleSocial.Data.Models
{
    public class PostReport
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }

        public SimpleSocialUser Author { get; set; }

        public int PostId { get; set; }

        public Post Post { get; set; }

        public ReportReason ReportReason { get; set; }

        public DateTime ReportedOn { get; set; } = DateTime.UtcNow;
    }
}
