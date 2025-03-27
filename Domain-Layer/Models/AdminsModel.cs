using System;
using System.Collections.Generic;
using System.Text;

namespace Domain_Layer.Models
{
    public class AdminsModel
    {
        public int Id { get; set; }

        public int AdminId { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
        public string DistrictName { get; set; }
        public string ProvinceName { get; set; }
        public string Location { get; set; }
        public string NrcNumber { get; set; }
        public UsersModel UserData { get; set; } = new UsersModel();
    }
}
