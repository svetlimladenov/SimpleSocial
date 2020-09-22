using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleSocial.Data.Models
{

    public class Page
    {
        public Page()
        {
            Posts = new HashSet<Post>();
        }
        public int Id { get; set; }

        public ICollection<Post> Posts { get; set; }

        [ForeignKey("PageAdmin")]
        public int PageAdminId { get; set; }

        public SimpleSocialUser PageAdmin { get; set; }
    }
}
