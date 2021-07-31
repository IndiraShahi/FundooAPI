using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommonLayer
{
    public class User
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string ConfirmPassword { get; set; }
        public static object NewPassword { get; set; }
        public string CreateDate { get; set; }
        public string ModifiedDate { get; set; }
    }
    public class Login
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
    
    public class ResetPassword
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}

