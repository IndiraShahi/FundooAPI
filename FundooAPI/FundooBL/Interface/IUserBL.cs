using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.Interface
{
    public interface IUserBL
    {
        
        User RegisterNewUser(User newUser);
        User UserLogin(string email , string password);
        User ForgotPassword(string Email);
        User ResetPassword(ResetPassword newPassword);
        string GenerateSecurityToken(string Email, long UserId);
    }
}
