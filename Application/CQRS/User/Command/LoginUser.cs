using Application.Common.Extentions;
using Application.Common.Interface;
using Application.Dto;
using Domain.Encryption;
using Domain.Entity.Token;
using Domain.Entity.Users;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.User.Command
{
    public class LoginUser:IRequest<IResponceBase>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginHandler : Result, IRequestHandler<LoginUser, IResponceBase>
    {
        private readonly IToken _token;
        private readonly IMainDbContext _mainDbContext;
        public LoginHandler(IMainDbContext dbContext,IToken token)
        {
            _mainDbContext = dbContext;
            _token = token;
        }

        public async Task<IResponceBase> Handle(LoginUser request, CancellationToken cancellationToken)
        {
            var user = await _mainDbContext.Users.FirstOrDefaultAsync(x => !x.IsDeleted && x.UserName == request.UserName || x.Email == request.UserName);
            if (user == null) { return await FalseOk(ResponceMessage.UserNotFound); }
            if (!user.IsActive) { return await FalseOk(ResponceMessage.UserNotActive); }
            if (Hasing.VerifyHash(request.Password, user.Password))
            {
                var token = await _token.GenerateAccessToken(user);
                return await TrueOk(new TokenDto()
                {
                    ExpireDate = token.Item4,
                    ExpireRefreshToken = token.Item3,
                    Token = token.Item1,
                    RefreshToken = token.Item2,
                });
            }
            else
            {
                return await FalseOk(ResponceMessage.WrongUserPass);
            }
            
        }
    }
}
