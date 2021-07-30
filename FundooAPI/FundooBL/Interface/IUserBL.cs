using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.Interface
{
    public interface IUserBL
    {
        
        User RegisterNewUser(User newUser);
        User UserLogin(Login login);
        User ForgotPassword(string userName);
        User ResetPassword(User existingUser, string password);
    }
}
