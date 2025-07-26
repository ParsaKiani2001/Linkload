using Application.Common.Extentions;
using Application.Common.Interface;
using Domain.Encryption;
using Domain.Entity.Users;
using Domain.Enums;
using MediatR;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.User.Command
{
    public class SignupUser:IRequest<IResponceBase>
    {
        public string name { get; set; }
        public string family { get; set; }

        public string email { get; set; }
        public string passwrod { get; set; }


    }

    public class SignupUserHandler : Result, IRequestHandler<SignupUser, IResponceBase>
    {
        private readonly IMainDbContext _mainDbContext;
        public SignupUserHandler(IMainDbContext context) {
            _mainDbContext = context;
        }
        public async Task<IResponceBase> Handle(SignupUser request, CancellationToken cancellationToken)
        {
            if(_mainDbContext.Users.Any(x=>x.Email == request.email))
            {
                return await FalseOk(ResponceMessage.ExistUserWhitEmail);
            }
            var time = DateTime.UtcNow;
            var user = await _mainDbContext.Users.AddAsync(
                new Domain.Entity.Users.User()
                {
                    UserName = request.name +"-"+ time.Year.ToString() + time.DayOfYear.ToString() + time.Hour.ToString()+time.Second.ToString()+time.Minute.ToString(),
                    Email = request.email,
                    IsActive = false,
                    Name = request.name,
                    Family = request.family,
                    Password = Hasing.ComputeHash(request.passwrod)
                }
                );
            var rang = RandomNumberGenerator.Create();

            if (user != null)
            {
                Random random = new Random();
                int randomNumber = random.Next(100000, 1000000);
           /*     await _mainDbContext.Otps.AddAsync(new Otp()
                {
                    code = randomNumber.ToString(),
                    ExpireDate = DateTime.SpecifyKind(DateTime.UtcNow.Add(TimeSpan.FromMinutes(7)),DateTimeKind.Utc),
                    IsUsed = false,
                    IsActive = true,
                    IsDeleted = false,
                    UserId = user.Entity.Id,
                });*/
            }
            else
            {
                return await FalseException(ResponceMessage.ServerError);
            }
            await _mainDbContext.SaveChangesAsync();
            return await Ok();

        }
    }
}
