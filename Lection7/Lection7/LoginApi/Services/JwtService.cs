using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LoginApi.Services
{
    public class JwtService
    {
        private readonly byte[] _key;
        private readonly double _expirationSeconds;
        
        public JwtService(IConfiguration configuration)
        {
            _key = Encoding.ASCII.GetBytes(configuration.GetSection("JwtConfig:Secret").Value);
            _expirationSeconds = double.Parse(configuration.GetSection("JwtConfig:ExpirationInSeconds").Value);
        }
        
        public string GetToken(string username, string lastname)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {  
                Subject = new ClaimsIdentity(new[]
                {  
                    new Claim("UserName", username),
                    new Claim("LastName", lastname)
                }),
                Expires = DateTime.UtcNow.AddSeconds(_expirationSeconds),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_key),
                                                            SecurityAlgorithms.HmacSha256Signature)
            };  
  
            var token = tokenHandler.CreateToken(tokenDescriptor);
  
            return tokenHandler.WriteToken(token); 
        }
    }
}