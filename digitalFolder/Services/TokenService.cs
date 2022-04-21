using DigitalFolder.Configuration;
using DigitalFolder.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DigitalFolder.Services
{
    public class TokenService
    {
        private TokenSettings _settings;

        public TokenService(IOptions<TokenSettings> settings)
        {
            _settings = settings.Value;
        }
        public string CreateToken(CustomIdentityUser user)
        {
            Claim[] userRights = new Claim[]
            {
                new Claim("username",user.UserName),
                new Claim("id",user.Id.ToString())
            };



            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(claims: userRights, signingCredentials: credentials, expires: DateTime.UtcNow.AddHours(_settings.ExpirationTime));
            var stringToken = new JwtSecurityTokenHandler().WriteToken(token);

            return stringToken;
        }   
    }
}
