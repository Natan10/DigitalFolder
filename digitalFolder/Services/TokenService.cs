﻿using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DigitalFolder.Services
{
    public class TokenService
    {
        public string CreateToken(IdentityUser<int> user)
        {
            Claim[] userRights = new Claim[]
            {
                new Claim("username",user.UserName),
                new Claim("id",user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("c64ee5a309c649efb8feead9c504a4c2"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(claims: userRights, signingCredentials: credentials, expires: DateTime.UtcNow.AddHours(1));
            var stringToken = new JwtSecurityTokenHandler().WriteToken(token);

            return stringToken;
        }   
    }
}
