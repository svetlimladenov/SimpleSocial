using Microsoft.AspNetCore.Mvc;
using SimpleSocial.Data;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SimpleSocial.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly SimpleSocialContext context;
        public PostController(SimpleSocialContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Posts()
        {
            var allPosts = this.context.Posts.ToList();

            return new JsonResult(allPosts);
        }

        //[HttpGet]
        //public async Task<IActionResult> Get(int id)
        //{
        //    var post = await this.context.Posts.FirstOrDefaultAsync(p => p.Id == id);
        //    return Ok(post);
        //}
    }
}
