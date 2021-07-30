//using Microsoft.SqlServer.Management.Smo;
//using System.Collections.Generic;

using CommonLayer;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        User RegisterNewUser(User newUser);
       
        User UserLogin(Login login);
        User ForgotPassword(string Email);
        User ResetPassword(User existingUser, string password);
    }
}
