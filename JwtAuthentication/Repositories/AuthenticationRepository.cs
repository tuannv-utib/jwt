using JwtAuthentication.Configure;
using JwtAuthentication.Models;
using JwtAuthentication.Repositories.Interface;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JwtAuthentication.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly List<UserModel> fakeUserData = new List<UserModel>() {
            new UserModel() {
                Username = "admin",
                Password = "admin" }
        };

        public async Task<string> GennerateJwtToken(UserModel model)
        {
            var user = fakeUserData.FirstOrDefault(c => c.Username == model.Username && c.Password == model.Password);
            if (user is null)
            {
                return "";
            }
            var token = await GenToken(user);
            return token;
        }

        private async Task<string> GenToken(UserModel model)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppKeys.JWTSecretKey);
            List<Claim> claim = new List<Claim>();
            claim.Add(new Claim(ClaimTypes.NameIdentifier, model.Username));
            claim.Add(new Claim(ClaimTypes.Role, "Role"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claim),
                Expires = DateTime.UtcNow.AddMinutes(AppKeys.JWTTimeout), // Expires after xx minutes
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenData = tokenHandler.CreateToken(tokenDescriptor);
            var tokenStr = tokenHandler.WriteToken(tokenData);
            return tokenStr;
        }
    }
}
