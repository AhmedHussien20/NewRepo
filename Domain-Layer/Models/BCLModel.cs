using System;
using System.Collections.Generic;
using System.Text;

namespace Domain_Layer.Models
{
    public class BCLModel
    {
        public string sTranNo { get; set; }
        public string rcpno { get; set; }
        public DateTime? dtDateTime { get; set; }
        public DateTime? dateverify { get; set; }
        public string sVendorID { get; set; }
        public string sVendorName { get; set; }
        public string PROVINCE { get; set; }
        public string DISTRICT { get; set; }
        public string SATELITE { get; set; }
        public string sItemNo { get; set; }
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
        public int bclStatus { get; set; } = 0;
        public string verifyUser { get; set; }
        public string DeviceSerialNumber { get; set; }
        public string dateModifyBy { get; set; }
        public string dateModify { get; set; }
    }

    public class PostBCLModelResponse
    {
        public string api_reference { get; set; }
        public Data data { get; set; }
        public string message { get; set; }
        public int status { get; set; }
        public bool success { get; set; }
        public int steps { get; set; }
    }

    public class Data
    {
        public string reference { get; set; }
        public DateTime time { get; set; }
    }

    public class PostBCLModel
    {
        public string sTranNo { get; set; }
        public string status { get; set; }
    }
}
