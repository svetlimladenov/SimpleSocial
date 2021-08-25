using Microsoft.AspNetCore.Mvc;
using SimpleSocial.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SimpleSocial.Api.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostController : ControllerBase
    {
        private readonly SimpleSocialContext context;
        public PostController(SimpleSocialContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetPosts()
        {
            var allPosts = this.context.Posts.ToList();

            return new JsonResult(allPosts);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await this.context.Posts.FirstOrDefaultAsync(p => p.Id == id);
            return Ok(post);
        }

        [HttpGet("/api/users/{userId:int}/posts")]
        public async Task<IActionResult> GetUserPosts(int userId)
        {
            var posts = await this.context.Posts
                .Where(x => x.UserId == userId && x.IsDeleted == false)
                .Select(x => new 
                {
                    x.Id,
                    x.Title,
                    x.Content,
                    x.CreatedOn
                })
                .ToListAsync();

            return Ok(posts);
        }
    }
}
