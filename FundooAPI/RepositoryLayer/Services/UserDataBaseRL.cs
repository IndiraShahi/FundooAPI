using CommonLayer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using RepositoryLayer.MSMQUtility;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

        public bool RegisterNewUser(User newUser)
        {
            try
            {
                if (newUser != null && newUser.Password.Equals(newUser.Password))
                {

                    newUser.CreateDate = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                    newUser.ModifiedDate = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                    fundooContext.FundooNotes.Add(newUser);
                    fundooContext.SaveChanges();
                    return true;
                }
            }
            catch
            {
                throw;
            }
            return false;
        }
        public Response UserLogin(string email, string password)
        {
            var user = fundooContext.FundooNotes.FirstOrDefault(user => user.Email == email && user.Password == password);

            if (user == null)
            {
                return null;
            }
            Response Response = new Response();

            Response.Token = GenerateSecurityToken(user.Email, user.UserId);
            Response.UserId = user.UserId;
            Response.FirstName = user.FirstName;
            Response.LastName = user.LastName;
            Response.Email = user.Email;
            return Response;

        }


        public bool ForgotPassword(string Email)
        {
            try
            {
                string user;

                var usr = this.fundooContext.FundooNotes.SingleOrDefault(x => x.Email == Email);

                if (usr != null)
                {
                    string token = GenerateSecurityToken(usr.Email, usr.UserId);
                    MSMQ msmq = new MSMQ();
                    msmq.SendMessage(Email, token);

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GenerateSecurityToken(string email, long UserId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email , email),
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
            var user = fundooContext.FundooNotes.FirstOrDefault(x => x.Email == email);

            if (user != null)
            {
                return user;
            }
            return null;
        }
        public bool ResetPassword(ResetPassword resetPassword)
        {
            var user = fundooContext.FundooNotes.SingleOrDefault(x => x.Email == resetPassword.Email);

            if (user != null)
            {
                user.Password = resetPassword.NewPassword;
                user.Password = resetPassword.ConfirmPassword;
                fundooContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}




