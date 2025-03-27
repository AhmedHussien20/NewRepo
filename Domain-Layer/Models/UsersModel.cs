using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain_Layer.Models
{
    public class UsersModel
    {
        public string SecurityStamp { get; set; } = Guid.NewGuid().ToString();
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool isDefaultPassword { get; set; }
        public bool EmailAuthorised { get; set; }
        public DateTime SuspendedDuration { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public int RoleId { get; set; }
        public int AccountStatusId { get; set; }
        public DateTime DOB { get; set; }
        public AccountStatusModel AccountStatus { get; set; } = new AccountStatusModel();
        public RolesModel RolesModel { get; set; } = new RolesModel();
        public string Province { get; set; }
        public string District { get; set; }
    }
}
