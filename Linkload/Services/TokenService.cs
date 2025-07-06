using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Application.Common.Extentions;
using Application.Common.Interface;
using Domain.Entity.Token;
using Domain.Entity.Users;
using Domain.Enums;
using System.Security.Claims;
using Infrastructure.JWT;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Linkload.API.Services
{
    public class TokenService : Result, IToken
    {
        private readonly TokenOptions _Options;
        private readonly TokenOptions tokenOptions;
        private readonly IMainDbContext _mainDbContext;
        public TokenService(TokenOptions options, TokenOptions tokenOptions, IMainDbContext mainDbContext)
        {
            _Options = options;
            this.tokenOptions = tokenOptions;
            _mainDbContext = mainDbContext;
        }

        public async Task<Tuple<string, string, DateTime>> GenerateAccessToken(User user)
        {
            var expiration = TimeSpan.FromMinutes(tokenOptions.TokenExpiryInMinutes);
            var jwt = new JwtSecurityToken(issuer: tokenOptions.Issuer,
                                           audience: tokenOptions.Audience,
                                           claims: MergeUserClaimsWithDefaultClaims(user),
                                           notBefore: DateTime.UtcNow,
                                           expires: DateTime.UtcNow.Add(expiration),
                                           signingCredentials: new SigningCredentials(
                                               tokenOptions.SigningKey,
                                               SecurityAlgorithms.HmacSha256));

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwt);
            var token = await _mainDbContext.Tokens.FirstOrDefaultAsync(x => x.UserId == user.Id);
            if (token != null)
            {
                token.Jwtid = jwt.Id;
                token.ExpireDate = DateTime.Now.AddMinutes(45);
                token.isUsed = true;
                token.UserId = user.Id;
                token.Token = accessToken;
                token.RefreshToken = token.RefreshToken ?? Guid.NewGuid().ToString();
                _mainDbContext.Tokens.Update(token);
            }
            else
            {
                token = new Tokens()
                {
                    Jwtid = jwt.Id,
                    ExpireDate = DateTime.Now.AddMinutes(45),
                    ExpireRefreshToken = DateTime.Now.AddDays(90),
                    RefreshToken = Guid.NewGuid().ToString(),
                    isUsed = true,
                    UserId = user.Id,
                    Token = accessToken

                };
                await _mainDbContext.Tokens.AddAsync(token);

            }
            await _mainDbContext.SaveChangesAsync();
            return Tuple.Create(accessToken, token.RefreshToken, token.ExpireRefreshToken);

        }

        private IEnumerable<Claim> MergeUserClaimsWithDefaultClaims(User user)
        {

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim("Family", user.Family),
                new Claim(ClaimTypes.Email, user.Email),

                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,
                    DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                    ClaimValueTypes.Integer64)
            };

            return claims;
        }

        public async Task<IResponceBase> ValidateToken(string token)
        {
            if(token == null || !token.Contains("Bearer"))return await FalseOk(Domain.Enums.ResponceMessage.UnAuthorized);
            var code = token.Split(' ').Last();
            if(string.IsNullOrEmpty(code)) return await FalseOk(Domain.Enums.ResponceMessage.UnAuthorized);
            try
            {
                var key = Encoding.UTF8.GetBytes(_Options.key);
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);
                var tokenExp = jwtSecurityToken.Claims.First(claim => claim.Type.Equals("exp")).Value;
                var ticks = long.Parse(tokenExp);
                var tokenDate = DateTimeOffset.FromUnixTimeSeconds(ticks).UtcDateTime;
                if (tokenDate < DateTime.UtcNow) return await FalseOk(ResponceMessage.AccTokenExpire);
                handler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                if (jwtToken.Claims?.Any() != true) return await FalseOk(ResponceMessage.UnAuthorized);
                var userId = jwtToken.Claims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId)) return await FalseOk(ResponceMessage.UnAuthorized);
                
                return await Ok();
            }catch(Exception ex)
            {
                return await FalseOk(ResponceMessage.UnAuthorized);
            }
        }
    }
}
