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
        
        public UserDataBaseRL(UserContext fundooContext, IConfiguration config)
        {
            this.fundooContext = fundooContext;
            
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
        public User UserLogin(string email , string password)
        {
            
            var user = fundooContext.FundooNotes.FirstOrDefault(user => user.Email == email && user.Password == password);
            
            if(user == null)
            {
                return null;
            }

            return user;
            
        }
        
        
        public User ForgotPassword(string Email)
        {
            throw new NotImplementedException();
        }

        public User ResetPassword(ResetPassword newPassword)
        {
            throw new NotImplementedException();
        }


        // public User ForgotPassword(string Email)
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

        //}

        // public User ResetPassword(string existingUser, string NewPassword)
        //{
        //        var result = fundooContext.FundooNotes.Where(i => i.Email == User).FirstOrDefault();
        //        if (result != null)
        //        {
        //            result.Password = (string)CommonLayer.User.NewPassword;
        //            fundooContext.FundooNotes.Update(result).Property(x => x.Email).IsModified = false;
        //            return User;
        //        }
        //        return null;

        //}

        //}

    }
}




