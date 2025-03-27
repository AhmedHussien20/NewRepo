using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain_Layer.Models
{
    public class FarmerKycModel
    {
        public string Mobile_Number { get; set; }
        public string Nrc_Number { get; set; }
        public string First_Name { get; set; }
        public string Other_Name { get; set; }
        public string Last_Name { get; set; }
        public string Date_Of_Birth { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public string Depot_Code { get; set; }
        public string Payment_Provider_Id { get; set; }
        public string Bank_Branch_Id { get; set; }
        public string Account_Number { get; set; }
    }

    public class ResponseModelKyc
    {
        public string Api_Reference { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
        public bool Success { get; set; }
    }

}
