using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Security.Service
{
    public class TokenService
    {
        private readonly IConfiguration configuration;

        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public TokenInfo GenerateToken(int userId, string username)
        {
            var tokenExpiration = GetTokenExpiration();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = GetSigningCredentials(),
                NotBefore = DateTime.UtcNow,
                Expires = tokenExpiration,
                Subject = new ClaimsIdentity(GetClaims(userId, username)),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var tokenString = tokenHandler.WriteToken(token);

            return new TokenInfo()
            {
                Token = tokenString,
                Expire = tokenExpiration
            };
        }
        private List<Claim> GetClaims(int userId, string username)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.AuthenticationMethod, JwtBearerDefaults.AuthenticationScheme),
            };

            return claims;
        }
        private SigningCredentials GetSigningCredentials()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecurityKey"]));
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
        }
        private DateTime GetTokenExpiration()
        {
            return DateTime.UtcNow.AddDays(Convert.ToDouble(configuration["Jwt:TokenExpirationDays"]));
        }
    }

    public class TokenInfo
    {
        public string Token { get; set; }
        public DateTime Expire { get; set; }
    }
}
