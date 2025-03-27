using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain_Layer.Models
{
    public class PaymentProviderModel
    {
        public string Api_Reference { get; set; }
        public List<DataModelPay> Data { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
        public bool Success { get; set; }
    }

    public class DataModelPay
    {
        public List<Branches> Branches { get; set; }
        public int Uid { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }

    public class Branches
    {
        public int Uid { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int PaymentProviderId { get; set; }
    }
}
