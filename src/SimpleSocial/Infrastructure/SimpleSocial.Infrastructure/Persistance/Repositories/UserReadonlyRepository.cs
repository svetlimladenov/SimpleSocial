using Microsoft.EntityFrameworkCore;
using SimpleSocial.Application.Repositories;
using SimpleSocial.Domain.Entities;
using SimpleSocial.Infrastructure.Persistance.Context;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleSocial.Infrastructure.Persistance.Repositories
{
    public class UserReadonlyRepository : IUserReadonlyRepository
    {
        private readonly WebDbContext context;

        public UserReadonlyRepository(WebDbContext context)
        {
            this.context = context;
        }

        public async Task<User> GetUserAsync(int id)
            => await this.context.Users.FindAsync(id);
    }
}
