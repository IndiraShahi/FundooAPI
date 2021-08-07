//using Microsoft.SqlServer.Management.Smo;
//using System.Collections.Generic;

using CommonLayer;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        bool RegisterNewUser(User newUser);
       
        Response UserLogin(string email , string password);
        bool ForgotPassword(string Email);
        bool ResetPassword(ResetPassword resetPassword);
        string GenerateSecurityToken(string email, long UserId);
        User GetUser(string email);
    }
}
