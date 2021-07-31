using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer
{
    public class Response
    {
        public long UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
    }
}
