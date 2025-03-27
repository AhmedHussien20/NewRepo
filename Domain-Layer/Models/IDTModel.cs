using System;
using System.Collections.Generic;
using System.Text;

namespace Domain_Layer.Models
{
    public class IDTModel
    {
        public string IDTNo { get; set; }
        //public string DIDTNo { get; set; }
        public DateTime IDTDate { get; set; } = DateTime.Now;
        public string LONumber { get; set; }
        public string FromLoc { get; set; }
        public string FromLocName { get; set; }
        public string ToLoc { get; set; }
        public string ToLocName { get; set; }
        public string sContrLoc { get; set; }
        public string GITLOC { get; set; }
        public string sItemNo { get; set; }
        public string sDesc { get; set; }
        public string sUnit { get; set; }
        public float SQtyTransBG { get; set; }
        public float SAvgWeig { get; set; }
        public float SQtyAvail { get; set; }
        public float SBagWeiged { get; set; }
        public string sVendor { get; set; }
        public string sVendorName { get; set; }
        public string Driver { get; set; }
        public string Id { get; set; }
        public string Vehicle { get; set; }
        public string IDTPreparedBy { get; set; }
        public string IDTRef { get; set; }
        public string IDTRefDate { get; set; }
        public string IDComplete { get; set; }
        public string IDTPrinted { get; set; }
        public string IDTIntegrate { get; set; }
        public string SerialNumber { get; set; }
        public string IDTModifyBy { get; set; }
        public string IDTModifyDate { get; set; }
        public int IDTSource { get; set; } = 1;
        public float RetractedTransBG { get; set; }
        public string IDTStatus { get; set; } = "Pending";
    }
}
