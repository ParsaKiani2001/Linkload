using Application.Common.Extentions;
using Application.Common.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.User.Command
{
    public class ReValidate:IRequest<IResponceBase>
    {
        public string username {  get; set; }
    }

    public class ReValidateHandler : Result, IRequestHandler<ReValidate,IResponceBase>
    {
        private readonly IMainDbContext _mainDbContext;
        public ReValidateHandler(IMainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task<IResponceBase> Handle(ReValidate request, CancellationToken cancellationToken)
        {
            if(await _mainDbContext.Users.AnyAsync(x=>x.UserName == request.username || x.Email == request.username && x.IsActive == false))
            {
                var user = await _mainDbContext.Users.FirstOrDefaultAsync(x => x.UserName == request.username || x.Email == request.username);
                Random random = new Random();
                int randomNumber = random.Next(100000, 1000000);
                await _mainDbContext.Otps.AddAsync(new Domain.Entity.Users.Otp()
                {
                    code = randomNumber.ToString(),
                    ExpireDate = DateTime.UtcNow.Add(TimeSpan.FromMinutes(7)),
                    IsUsed = false,
                    IsActive = true,
                    IsDeleted = false,
                    UserId = user.Id,
                });
                await _mainDbContext.SaveChangesAsync();
                return await Ok();
            }
            return await FalseOk(Domain.Enums.ResponceMessage.UserNotFound);
        }
    }
}
