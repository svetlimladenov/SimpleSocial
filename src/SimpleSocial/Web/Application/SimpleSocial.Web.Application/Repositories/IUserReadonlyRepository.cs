using SimpleSocial.Domain.Entities;
using System.Threading.Tasks;

namespace SimpleSocial.Application.Repositories
{
    public interface IUserReadonlyRepository
    {
        Task<User> GetUserAsync(int id);
    }
}
