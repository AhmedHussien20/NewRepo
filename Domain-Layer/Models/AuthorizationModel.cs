using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain_Layer.Models
{
    public class AuthorizationModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }

}
