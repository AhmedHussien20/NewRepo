using System;
using System.Collections.Generic;
using System.Text;

namespace Domain_Layer.Models
{
    public class LoadingOrdersModel
    {
        public string LONumber { get; set; }
        public string LODate { get; set; }
        public string ReqNum { get; set; }
        public string FromLoc { get; set; }
        public string FromLocName { get; set; }
        public string ToLoc { get; set; }
        public string ToLocName { get; set; }
        public string sContrLoc { get; set; }
        public string sItemNo { get; set; }
        public string sDesc { get; set; }
        public string sUnit { get; set; }
        public float SQtyBags { get; set; }
        public float SQtyBagsMoved { get; set; }
        public float SQtyMT { get; set; }
        public string sVendor { get; set; }
        public string sVendorName { get; set; }
        public string SVenAdr1 { get; set; }
        public float Distance { get; set; }
        public float Rate { get; set; }
        public string RateType { get; set; }
        public float AmtBfrVAT { get; set; }
        public float AmtVAT { get; set; }
        public float TOTAL { get; set; }
        public string Vatable { get; set; }
        public string LOPreparedBy { get; set; }
        public string LOComplete { get; set; }
        public string LOonhold { get; set; }
        public string Printed { get; set; }
        public string ManualLO { get; set; }
    }
}
