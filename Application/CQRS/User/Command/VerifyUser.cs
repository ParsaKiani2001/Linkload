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
    public class VerifyUser:IRequest<IResponceBase>
    {
        public string otpCode {  get; set; }
        public string username { get; set; }
    }

    public class VerifyUserHandler :Result, IRequestHandler<VerifyUser, IResponceBase>
    {
        private readonly IMainDbContext mainDbContext;
        public VerifyUserHandler(IMainDbContext _mainDbContext) {
            mainDbContext = _mainDbContext;
        }
        public async Task<IResponceBase> Handle(VerifyUser request, CancellationToken cancellationToken)
        {
            if(!await mainDbContext.Users.AnyAsync(x=>x.IsActive == false && x.Email == request.username || x.UserName == request.username))
            {
                return await FalseOk(Domain.Enums.ResponceMessage.UserNotFound);
            }
            if (await mainDbContext.Otps.AnyAsync(x => x.IsUsed == false & x.code == request.otpCode))
            {
                var otp = await mainDbContext.Otps.Include(x => x.User).FirstOrDefaultAsync(x => x.IsUsed == false && x.code == request.otpCode && x.User.UserName == request.username || x.User.Email == request.username);
                var user = await mainDbContext.Users.FirstOrDefaultAsync(x => x.Id == otp.UserId);
                if (otp.ExpireDate > DateTime.UtcNow)
                {
                    return await FalseOk(Domain.Enums.ResponceMessage.OtpWrong);
                }
                else
                {
                    otp.IsUsed = true;
                    otp.IsActive = false;
                    mainDbContext.Otps.Update(otp);
                    user.IsActive = true;
                    mainDbContext.Users.Update(user);
                    await mainDbContext.SaveChangesAsync();
                    return await Ok();
                }
            }
            return await FalseOk(Domain.Enums.ResponceMessage.OtpWrong);
        }
    }
}
