using System;
using System.Threading.Tasks;

namespace SimpleSocial.Data.Seeding
{
    interface ISeeder
    {
        Task SeedAsync(SimpleSocialContext dbContext, IServiceProvider serviceProvider);
    }
}
