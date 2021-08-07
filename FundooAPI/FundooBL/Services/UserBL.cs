using BuisnessLayer.Interface;
using CommonLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.SqlServer.Management.SqlParser.Metadata;
using RepositoryLayer.Interface;
using RepositoryLayer.MSMQUtility;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
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

        
        public Response UserLogin(string email , string password)
        {
            return userRL.UserLogin(email, password);

        }
        
       
        public bool RegisterNewUser(User newUser)
        {
            try
            {
                userRL.RegisterNewUser(newUser);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ForgotPassword(string Email)
        {
            try
            {
                return userRL.ForgotPassword(Email);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ResetPassword(ResetPassword resetPassword)
        {
            try
            {
                return userRL.ResetPassword(resetPassword);
            }
            catch (Exception)
            {
                throw;
            }
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
        public User GetUser(string email)
        {
            return userRL.GetUser(email);
        }
    }
}
