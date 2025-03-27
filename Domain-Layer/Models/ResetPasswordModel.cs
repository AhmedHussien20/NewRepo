using System;
using System.Collections.Generic;
using System.Text;

namespace Domain_Layer.Models
{
    public class ResetPasswordModel
    {
        public string defaultPasswoord { get; set; }
        public string newPassword { get; set; }
        public int userId { get; set; }
    }
}
