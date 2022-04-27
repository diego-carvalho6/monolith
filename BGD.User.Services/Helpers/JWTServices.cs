using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BGD.User.Services.Helpers
{
    public class JWTServices
    {
        private IConfiguration _config;
        public JWTServices(IConfiguration Configuration)
        {
            _config = Configuration;
        }

        public string GenerateTokenJWT(dynamic user, string tenant = "default")
        {
            var claims = new Claim[]
            {
                new Claim("Name", user.Username),
                new Claim("Id", user.Id.ToString()),
                new Claim("Tenant", tenant),
                new Claim(ClaimTypes.Role, RoleFactory(user.Status))
                
          };
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var expiry = DateTime.Now.AddMinutes(120);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: issuer,audience: audience,
                expires: DateTime.Now.AddMinutes(1200),signingCredentials: credentials, claims: claims);
            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }
        public string GenerateTokenJWTAnonymous(string table, string tenant = "default")
        {
            var claims = new Claim[]
            {
                new Claim("Name", "anonymous"),
                new Claim("Id", "anonymous"),
                new Claim("Tenant", tenant),
                new Claim("Table", table),
                new Claim(ClaimTypes.Role, "anonymous")
                
            };
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];
            var expiry = DateTime.Now.AddMinutes(120);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(issuer: issuer,audience: audience,
                expires: DateTime.Now.AddMinutes(10),signingCredentials: credentials, claims: claims);
            var tokenHandler = new JwtSecurityTokenHandler();
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }

        private static string RoleFactory(int roleNumber)
        {
            switch (roleNumber)
            {
                case 0:
                    return "Anonymous";
                case 1:
                    return "LoggedUser";
                case 2:
                    return "Employee";
                case 3:
                    return "Staff";
                case 4:
                    return "Admin";
                case 5:
                    return "AdminGlobal";
                default:
                    throw new Exception();
            }
        }
    }
}