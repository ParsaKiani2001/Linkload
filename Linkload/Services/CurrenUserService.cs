﻿using System.IdentityModel.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Application.Common.Interface;
using Application.Models;
using Infrastructure.JWT;
using Microsoft.IdentityModel.Tokens;

namespace Linkload.API.Services
{
    public class CurrenUserService : ICurrentUser
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly TokenOptions _tokenOptions;
        public CurrenUserService(TokenOptions options, IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
            _tokenOptions = options;
        }
        public UserModel UserModel => throw new NotImplementedException();
        private UserModel getUserModel()
        {
            var jwt = _contextAccessor.HttpContext?.Request.Headers.SingleOrDefault(o => o.Key == "Authorization").Value.FirstOrDefault();
            if (jwt == null) return null;
            var token = jwt.Split(' ').Last();
            var key = Encoding.UTF8.GetBytes(_tokenOptions.key);
            var handler = new JwtSecurityTokenHandler();
            try
            {
                handler.ValidateToken(token,new Microsoft.IdentityModel.Tokens.TokenValidationParameters{
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer=true,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                },out SecurityToken validationToken);
                var jwtToken = (JwtSecurityToken)validationToken;
                var id = jwtToken.Claims.First(x=>x.Type == ClaimTypes.NameIdentifier).Value;
                var name = jwtToken.Claims.First(o => o.Type == ClaimTypes.Name).Value;
                var family = jwtToken.Claims.FirstOrDefault(o => o.Type == "Family").Value;
                var email = jwtToken.Claims.FirstOrDefault(o => o.Type == ClaimTypes.Email).Value;
                return new UserModel { UserName = name, Email = email, Family = family,Id = Guid.Parse(id),Name = name};
            }
            catch (Exception ex) {
                return null;
            }
        }

    }
}
