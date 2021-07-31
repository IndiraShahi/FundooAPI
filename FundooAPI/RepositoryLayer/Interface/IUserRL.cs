//using Microsoft.SqlServer.Management.Smo;
//using System.Collections.Generic;

using CommonLayer;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        User RegisterNewUser(User newUser);
       
        User UserLogin(string email , string password);
        User ForgotPassword(string Email);
        User ResetPassword(ResetPassword newPassword);
    }
}
