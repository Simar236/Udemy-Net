using System;
using System.Text;
using System.Security.Cryptography;
using API.Entity;
using API.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace API.services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration Config)
        {
            _key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["TokenKey"]));
        }
        public string CreateToken(AppUser user)
        {
            //Token Creation updation
            var tokenHandler = new JwtSecurityTokenHandler();
            //list of claims you want to add in token
            var claims= new List<Claim>(){
                new Claim(JwtRegisteredClaimNames.NameId,user.UserName)
            };
            //encryption algorithm
            var creds = new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);
            //token model
            var tokenDescriptor = new SecurityTokenDescriptor(){
                Subject= new ClaimsIdentity(claims),
                Expires= DateTime.Now.AddDays(7),
                SigningCredentials=creds
            };
            //returns a security token
            var token= tokenHandler.CreateToken(tokenDescriptor);
            //converts token to string
            return tokenHandler.WriteToken(token);
        }
    }
}