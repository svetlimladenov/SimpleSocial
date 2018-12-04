using System.Threading.Tasks;
using SimpleSocial.Web.Models;

namespace SimpleSocial.Web.Services
{
    public interface IPostServices
    {
        Task CreatePost(MyProfileViewModel viewModel);
    }
}
