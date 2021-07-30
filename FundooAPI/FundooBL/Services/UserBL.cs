using BuisnessLayer.Interface;
using CommonLayer;
using Microsoft.SqlServer.Management.SqlParser.Metadata;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.Services
{
    public class UserBL : IUserBL
    {
        private IUserRL userRL;
        
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        
        public User UserLogin(Login login)
        {
            User user = null;
            if (login != null)
                user = userRL.UserLogin(login);
            if (user != null)
            {
                return user.Password.Equals(login.Password) ? user : null;
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

        public User ForgotPassword(string userName)
        {
            throw new NotImplementedException();
        }

        public User ResetPassword(User existingUser, string password)
        {
            throw new NotImplementedException();
        }
    }
}
