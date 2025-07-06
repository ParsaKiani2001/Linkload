using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.JWT
{
    public class TokenOptions
    {
        public TokenOptions(string issuer,
            string audience,
            string rawSigningKey,
            int tokenExpiryInMinutes = 45)
        {
            if (string.IsNullOrWhiteSpace(audience))
            {
                throw new ArgumentNullException(
                    $"{nameof(Audience)} is mandatory in order to generate a JWT!");
            }

            if (string.IsNullOrWhiteSpace(issuer))
            {
                throw new ArgumentNullException(
                    $"{nameof(Issuer)} is mandatory in order to generate a JWT!");
            }

            Audience = audience;
            Issuer = issuer;
            key = rawSigningKey;
            SigningKey = new SymmetricSecurityKey(
                             Encoding.ASCII.GetBytes(rawSigningKey)) ??
                         throw new ArgumentNullException(
                             $"{nameof(SigningKey)} is mandatory in order to generate a JWT!");

            TokenExpiryInMinutes = tokenExpiryInMinutes;
        }

        public SecurityKey SigningKey { get; }

        public string Issuer { get; }

        public string Audience { get; }

        public int TokenExpiryInMinutes { get; }
        public string key { get; }
    }


    public struct TokenConstants
        {
            public const string TokenName = "jwt";
        }
   
}
