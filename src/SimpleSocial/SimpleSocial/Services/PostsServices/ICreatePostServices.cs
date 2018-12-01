using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleSocial.ViewModels.Account;

namespace SimpleSocial.Services.PostsServices
{
    public interface ICreatePostServices
    {
        void CreatePost(MyProfileViewModel viewModel);
    }
}
