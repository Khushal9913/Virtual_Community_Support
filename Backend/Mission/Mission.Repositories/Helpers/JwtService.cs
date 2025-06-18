using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Repositories.Helpers
{
    public class JwtService
    {
        public string SecretKey { get; set; }
        public int TokenDuration { get; set; }

        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
            SecretKey = configuration.GetSection("JwtConfig").GetSection("Key").Value;
            TokenDuration = int.Parse(configuration.GetSection("JwtConfig").GetSection("Duration").Value);
        }

        public string GenerateToken(string userId, string firstName, string lastName, string phoneNumber, string emailAddress, string userType, string userImage)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var signature = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var payload = new[]
            {
                new Claim("userId",userId),
                new Claim("fullName",firstName+" "+lastName),
                new Claim("firstName",firstName),
                new Claim("lastName",lastName),
                new Claim("phoneNumber",phoneNumber),
                new Claim("emailAddress",emailAddress),
                new Claim("userType",userType),
                new Claim("userImage",userImage)
            };

            var jwtToken = new JwtSecurityToken(
                issuer: "localhost",
                audience: "localhost",
                claims: payload,
                expires: DateTime.Now.AddHours(TokenDuration),
                signingCredentials: signature
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
