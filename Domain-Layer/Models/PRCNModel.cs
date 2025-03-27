using System;
using System.Collections.Generic;
using System.Text;

namespace Domain_Layer.Models
{
    public class PRCNModel
    {
        public string sTranNo { get; set; }
        public string rcpno { get; set; }
        public DateTime? dtDateTime { get; set; }
        public DateTime? dateverify { get; set; }
        public string sVendorID { get; set; }
        public string sVendorName { get; set; }
        public string GinBagType { get; set; }
        public string PROVINCE { get; set; }
        public string DISTRICT { get; set; }
        public string SATELITE { get; set; }
        public string sItemNo { get; set; }
        public string terminal { get; set; }
        public string sItemDescr { get; set; }
        public string sUnit { get; set; }
        public float NoOfBags { get; set; }
        public float sUnitCost { get; set; }
        public float dDocTotal { get; set; }
        public float dDocTotalUsd { get; set; }
        public float dDocRate { get; set; }
        public int Type { get; set; }
        public string sGrainType { get; set; }
        public int bclpost { get; set; }
        public int bcllisting { get; set; }
        public decimal RetractedAmount { get; set; }
        public string Status { get; set; } = "Pending";
        public string verifyUser { get; set; }
        public string DeviceSerialNumber { get; set; }
        public string dateModifyBy { get; set; }
        public string dateModify { get; set; }
    }
}
