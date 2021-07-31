using CommonLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
     public class UserDataBaseRL : IUserRL
    {
        private UserContext fundooContext;
        private readonly string _secret;
        public UserDataBaseRL(UserContext fundooContext, IConfiguration config)
        {
            this.fundooContext = fundooContext;
            this._secret = config.GetSection("AppSettings").GetSection("Key").Value;
        }

        public User RegisterNewUser(User newUser)
        {
            if (newUser != null && newUser.Password.Equals(newUser.ConfirmPassword))
            {
               
                newUser.CreateDate = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                newUser.ModifiedDate = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                fundooContext.FundooNotes.Add(newUser);
                fundooContext.SaveChanges();
                return newUser;
            }
            return null;
        }
        public User UserLogin(Login login)
        {
            var user = fundooContext.FundooNotes.FirstOrDefault(user => user.Email == login.Email);
            if(user == null)
            {
                return null;
            }
            return user;
        }
        
        public string GenerateSecurityToken(string Email, int id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, Email),
                    new Claim("userId", id.ToString(), ClaimValueTypes.Integer)

       

                }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            string jwttoken = tokenHandler.WriteToken(token);
            return jwttoken;
        }

        //public User ForgotPassword(string Email)
        //{
        //    var result = fundooContext.FundooNotes.Where(i => i.Email == user).FirstOrDefault();
        //    if (result != null)
        //    {
        //        ResetPassword(User, result.FirstName);
        //        return Email;

        //    }
        //    else
        //    {
        //        return null;
        //    }

        //    public User ResetPassword(string existingUser, string NewPassword)
        //    {
        //        var result = fundooContext.FundooNotes.Where(i => i.Email == User).FirstOrDefault();
        //        if (result != null)
        //        {
        //            result.Password = (string)CommonLayer.User.NewPassword;
        //            fundooContext.FundooNotes.Update(result).Property(x => x.Email).IsModified = false;
        //            return User;
        //        }
        //        return null;
                
        //    }

        //}

        public User ResetPassword(User existingUser, string password)
        {
            throw new NotImplementedException();
        }

        public User ForgotPassword(string Email)
        {
            throw new NotImplementedException();
        }
    }
}



