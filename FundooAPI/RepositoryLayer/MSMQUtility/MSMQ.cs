using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.MSMQUtility
{
    public class MSMQ
    {

        MessageQueue messageQueue = new MessageQueue();

        private readonly string _secret;

        public MSMQ(IConfiguration config)
        {
            this._secret = config.GetSection("AppSettings").GetSection("Key").Value;
        }

        public MSMQ()
        {
        }

        public void SendMessage(string Email ,string token)
        {
            
            if (MessageQueue.Exists(@".\Private$\MyQueue"))
            {
                messageQueue = new MessageQueue(@".\Private$\MyQueue");
            }
            else
            {
                messageQueue = MessageQueue.Create(@".\Private$\MyQueue");
            }
            Message message = new Message();
            message.Formatter = new BinaryMessageFormatter();
            messageQueue.ReceiveCompleted += ReceiveCompletedMSMQ;
            message.Body = token;
            messageQueue.Label = "url link";
            messageQueue.Send(message);
            messageQueue.BeginReceive();
            messageQueue.Close();
        }

        private void ReceiveCompletedMSMQ(object sender, ReceiveCompletedEventArgs e)
        {
            var message = messageQueue.EndReceive(e.AsyncResult);
            string mailSubject = "Fundoo notes account password reset";
            message.Formatter = new BinaryMessageFormatter();
            string token = message.Body.ToString();
            string email = UserDataFromToken(token);
            var url = $"Link to reset your password : https://localhost:44359/api/users/resetpassword/{token}";
            using (MailMessage mailMessage = new MailMessage("indirashahi144@gmail.com", email))
            {
                mailMessage.Subject = mailSubject;
                mailMessage.Body = url;
                mailMessage.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("indirashahi144@gmail.com", "1234567");
                smtp.Port = 587;
                smtp.Send(mailMessage);
            }
        }

        public string UserDataFromToken(string token)
        {
            var key = Encoding.ASCII.GetBytes("Its good to see yourself grow everyday");
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            TokenValidationParameters parameters = new TokenValidationParameters
            {

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false

            };
            SecurityToken securityToken;
            ClaimsPrincipal principal;
            try
            {
                principal = tokenHandler.ValidateToken(token, parameters, out securityToken);
                string userEmail = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;
                var userId = Convert.ToInt32(principal.Claims.SingleOrDefault(c => c.Type == "userId").Value);
                return userEmail;
            }
            catch
            {
                principal = null;
            }
            return null;
        }
    }
}
