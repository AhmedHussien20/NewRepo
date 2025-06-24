using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain_Layer.Models
{
    public class FarmerModel
    {
        public FarmerModel()
        {
            this.PaymentMode = "Cash";
            this.BankName = string.Empty;
            this.BranchName = string.Empty;
            this.BranchCode = string.Empty;
            this.AccountName = string.Empty;
            this.AccountNumber = string.Empty;
            this.ModifyBy = string.Empty;
            this.ModifyOn = string.Empty;
        }

        public int Id { get; set; }
        public string Names { get; set; }
        public string NRCNumber { get; set; }
        public string FarmerCode { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string CreatedBy { get; set; }
        public bool Verified { get; set; }
        public string VerifiedString { get; set; } = null;
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string Tpin { get; set; }
        public string DOB { get; set; }
        public string SerialNumber { get; set; }

        [DefaultValue("Cash")]
        public string PaymentMode { get; set; }
        public string PaymentProviderId { get; set; }
        #nullable enable
        public string? BankBranchId { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string BranchCode { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string ModifyBy { get; set; }
        public string ModifyOn { get; set; }
    }
}
