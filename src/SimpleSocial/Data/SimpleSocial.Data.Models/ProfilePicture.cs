using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleSocial.Data.Models
{
    public class ProfilePicture
    {
        public string Id { get; set; }

        public string FileName { get; set; }

        public string UserId { get; set; }

        public SimpleSocialUser User { get; set; }
    }
}
