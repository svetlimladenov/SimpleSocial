using MediatR;
using SimpleSocial.Application.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleSocial.Application.Users.Queries
{
    public class GetUserInfoQuery : IRequest<UserBoxInfoModel>
    {
        public int UserId { get; set; }

        public class GetUserInfoHanlder : IRequestHandler<GetUserInfoQuery, UserBoxInfoModel>
        {
            private readonly IUserReadonlyRepository userRepository;

            public GetUserInfoHanlder(IUserReadonlyRepository userRepository)
            {
                this.userRepository = userRepository;
            }

            public async Task<UserBoxInfoModel> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
            {
                var user = await userRepository.GetUserAsync(request.UserId);
                var model = new UserBoxInfoModel()
                {
                    Username = user.Username,
                    FullName = $"{user.FirstName} {user.LastName}", // move this to the domain model
                    Age = CalculateAge(user.BirthDay),
                    Birthday = user.BirthDay,
                    JoinedOn = user.CreatedOn,
                    Location = $"{user.City} {user.Country}",
                    ProfilePictureUrl = user.ProfilePictureURL
                };

                throw new Exception("booomara");
                
                return model;
            }

            private int? CalculateAge(DateTime? birthdate)
            {
                if (!birthdate.HasValue)
                {
                    return null;
                }

                DateTime zeroTime = new DateTime(1, 1, 1);
                TimeSpan diff = DateTime.UtcNow.Subtract(birthdate.Value);
                int years = (zeroTime + diff).Year - 1;
                return years;
            }
        }
    }
}
