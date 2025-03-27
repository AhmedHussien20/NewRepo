using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain_Layer.Models
{
    public class DevicesAuthModel
    {
        public string Api_Reference { get; set; }
        public DataModel Data { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
        public bool Success { get; set; }
    }

    public class DataModel
    {
        public int Exp_Time { get; set; }
        public string Exp_Time_Type { get; set; }
        public string Token { get; set; }
    }
}
