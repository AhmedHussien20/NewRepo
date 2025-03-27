using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain_Layer.Models
{
    public class StaffModel
    {
        public int Id { get; set; }

        public int StaffId { get; set; }

        public string NRCNumber { get; set; }
        public string Location { get; set; }
        public UsersModel StaffData { get; set; } = new UsersModel();
        public netPurLocationModel LocationData { get; set; } = new netPurLocationModel();
    }
}
