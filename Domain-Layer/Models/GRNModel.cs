using System;
using System.Collections.Generic;
using System.Text;

namespace Domain_Layer.Models
{
    public class GRNModel
    {
        public string GRNTNo { get; set; }
        public DateTime GRNTDate { get; set; } = DateTime.Now;
        public string IDTNo { get; set; }
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
        public float SQtyGRNBG { get; set; }
        public float SAvgWeig { get; set; }
        public float SBagWeiged { get; set; }
        public float QtyVariance { get; set; }
        public float balance { get; set; }
        public string sVendor { get; set; }
        public string sVendorName { get; set; }
        public string GrnDriver { get; set; }
        public string GrnId { get; set; }
        public string GrnVehicle { get; set; }
        public string GrnParedBy { get; set; }
        public string GrnRef { get; set; }
        public string GRNRefDate { get; set; }
        public string GrnComplete { get; set; }
        public string GrnPosted { get; set; }
        public string GrnPrinted { get; set; }
        public string GRNIntegrate { get; set; }
        public string SerialNumber { get; set; }
        public string GRNModifyBy { get; set; }
        public string GRNModifyDate { get; set; }
        public int GRNSource { get; set; } = 1;
    }
}
