using BuisnessLayer.Interface;
using CommonLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Management.SqlParser.Metadata;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundooAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        [HttpPost("Register")]
        public ActionResult RegisterNewUser(User newUser)
        {
            try
            {
                userBL.RegisterNewUser(newUser);
                return Ok(new { success = true, message = "User Registered successfully", newUser.Email, newUser.FirstName, newUser.LastName });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }

        }

        

        [HttpPost("Login")]
        public ActionResult Login(Login login)
        {
            try
            {
                var user = userBL.UserLogin(login.Email, login.Password);
                if (user != null)
                {
                    string token = userBL.GenerateSecurityToken(user.Email, user.UserId);
                    return Ok(new { Success = true, Message = $"Login Successfull", Token = token });
                }
                return BadRequest(new { message = "Please enter correct email or password" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
                
            }
            
        }

        [HttpPost("forgotpassword")]
        public ActionResult Forgotpassword(ForgetPassword Email)
        {
            try
            {
                userBL.ForgotPassword(Email.Email);
                return Ok(new { message = "Link sent to the mail!" });
            }
            catch 
            {
                return BadRequest(new { message = "User does not exist!" });
            }
        }

        [HttpPut("resetpassword")]
        public ActionResult ResetPassword(ResetPassword resetPassword)
        {
            try
            {
                var user = userBL.ResetPassword(resetPassword);
                return Ok(new { message = "Password reset successful", Data = resetPassword.Email });
            }
            catch 
            {
                return BadRequest(new { message = "Email dosen't exist" });
            }
        }
    }
}
