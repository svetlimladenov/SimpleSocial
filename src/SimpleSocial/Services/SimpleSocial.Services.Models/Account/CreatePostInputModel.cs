using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using SimpleSocial.Data.Common;
using SimpleSocial.Data.Models;
using SimpleSocial.Services.Mapping;

namespace SimpleSocial.Services.Models.Account
{
    public class CreatePostInputModel : IMapTo<Post>, IValidatableObject
    {
        public string Title => string.IsNullOrEmpty(Content) ? string.Empty : Content.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];

        public string Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string WallId { get; set; }

        public int Likes { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var userRepository = (IRepository<SimpleSocialUser>)validationContext
                .GetService(typeof(IRepository<SimpleSocialUser>));

            var wallRepository = (IRepository<Wall>)validationContext
                .GetService(typeof(IRepository<Wall>));

            if (!userRepository.All().Any(x => x.Id == this.UserId))
            {
                yield return new ValidationResult("Invalid user id");
            }

            if (!wallRepository.All().Any(x => x.Id == this.WallId))
            {
                yield return new ValidationResult("Invalid wall id");
            }
        }
    }   
}
