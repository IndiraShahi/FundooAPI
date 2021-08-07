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
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [RegularExpression("^[A-Za-z0-9+.-]{3,20}@[A-Za-z0-9]{1,10}.+(com|co.in|net|com.au)$", ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string CreateDate { get; set; }
        public string ModifiedDate { get; set; }
        public List<Notes> Notes { get; set; }
    }
}

