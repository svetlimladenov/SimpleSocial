using System.Threading.Tasks;
using AutoMapper;
using SimpleSocial.Data;
using SimpleSocial.Services.Models.Users;

namespace SimpleSocial.Services.DataServices.Account
{
    public class MyProfileServices : IMyProfileServices
    {
        private readonly SimpleSocialContext dbContext;
        private readonly IMapper mapper;

        public MyProfileServices(
            SimpleSocialContext dbContext,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<UserInfoViewModel> GetUserInfo(string userId)
        {
            var user = await this.dbContext.Users.FindAsync(userId);
            var userInfo = this.mapper.Map<UserInfoViewModel>(user);
            return userInfo;
        }
    }
}
