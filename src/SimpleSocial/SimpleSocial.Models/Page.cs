using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleSocial.Models
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
        public string PageAdminId { get; set; }

        public User PageAdmin { get; set; }
    }
}
