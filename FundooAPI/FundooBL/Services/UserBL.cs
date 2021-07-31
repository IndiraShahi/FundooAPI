using BuisnessLayer.Interface;
using CommonLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SqlServer.Management.SqlParser.Metadata;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BuisnessLayer.Services
{
    public class UserBL : IUserBL
    {
        private IUserRL userRL;
        private readonly string _secret;

        public UserBL(IUserRL userRL , IConfiguration config)
        {
            this.userRL = userRL;
            this._secret = config.GetSection("AppSettings").GetSection("Key").Value;
        }

        
        public User UserLogin(string email , string password)
        {
            User user = null;
            if (email != null && password != null)
                user = userRL.UserLogin(email , password);
            if (user != null)
            {
                return user;
            }
            return null;
            
        }
        
       
        public User RegisterNewUser(User newUser)
        {
            if (newUser != null && newUser.Password.Equals(newUser.ConfirmPassword))
            {
                var user = userRL.RegisterNewUser(newUser);
                return user;
            }
            return null;

        }

        public User ForgotPassword(string Email)
        {
           

            throw new NotImplementedException();
        }

        public User ResetPassword(ResetPassword newPassword)
        {
            
            throw new NotImplementedException();
        }

        public string GenerateSecurityToken(string Email, long UserId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, Email),
                    new Claim("userId", UserId.ToString(), ClaimValueTypes.Integer)



                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string jwttoken = tokenHandler.WriteToken(token);
            return jwttoken;
        }
    }
}
