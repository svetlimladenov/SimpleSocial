using Microsoft.AspNetCore.Mvc;
using SimpleSocial.Data;
using System;
using System.Linq;

namespace SimpleSocial.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly SimpleSocialContext context;

        public UsersController(SimpleSocialContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(SimpleSocialContext));
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = this.context.Users.ToList();
            return Ok(users);
        }

        [HttpGet("{id:int}")] 
        public IActionResult GetUser(int id)
        {
            var user = this.context.Users
                .Where(x => x.Id == id)
                .Select(x => new
                {
                    x.FirstName,
                    x.LastName,
                    x.Id
                }).FirstOrDefault();

            if(user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }
    }
}
