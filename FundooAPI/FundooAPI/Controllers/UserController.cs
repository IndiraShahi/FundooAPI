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

        [HttpPost]
        [Route("Register")]
        public ActionResult RegisterNewUser(User newUser)
        {
            User user = userBL.RegisterNewUser(newUser);
            if (user != null)
                return Created(newUser.Email, user);
            return BadRequest("User Already Exists!!");
        }

        

        [HttpPost]
        [Route("Login")]
        public ActionResult Login(Login login)
        {
            var user = userBL.UserLogin(login.Email,login.Password);
            if (user != null)
            {
                string token = userBL.GenerateSecurityToken(user.Email, user.UserId);
                return Ok(new { Success = true, Message = $"Login Successfull",Token = token});
            }
            return NotFound("Invalid UserName or Password");
        }

       

        [HttpPost]
        [Route("forgotpassword")]
        public ActionResult Forgotpassword(User user)
        {
            
            throw new NotImplementedException();
        }

        [HttpPut]
        [Route("resetpassword")]
        public ActionResult ResetPassword(ResetPassword resetPassword)
        {
            throw new NotImplementedException();
        }
    }
}
