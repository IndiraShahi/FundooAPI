using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.Interface
{
    public interface IUserBL
    {
        
        bool RegisterNewUser(User newUser);
        Response UserLogin(string email , string password);
        bool ForgotPassword(string Email);
        bool ResetPassword(ResetPassword resetPassword);
        string GenerateSecurityToken(string Email, long UserId);
        User GetUser(string email);
    }
}
