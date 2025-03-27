using System;
using System.Collections.Generic;
using System.Text;

namespace Domain_Layer.Models
{
    public class GINDetailsModel
    {
        public int OrderID { get; set; }
        public int OrderType { get; set; }
        public int OrderDetailID { get; set; }
        public string ItemNum { get; set; }
        public string ItemDescr { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        public string UnitType { get; set; }
        public double Discount { get; set; }
        public double TaxRate { get; set; }
        public string AltItem { get; set; }
        public double LineTotal { get; set; }
        public double LineVat { get; set; }
        public string SFMTItemNo { get; set; }
        public string SQtyOrdered { get; set; }
        public int NDiscType { get; set; }
        public string SCatCode { get; set; }
        public double DQtyBO { get; set; }
        public int LCNReasonID { get; set; }
        public string LCNComment { get; set; }
        public int LType { get; set; }
        public string SSerialNo { get; set; }
        public string SComment { get; set; }
        public string SSalesmanNo { get; set; }
        public string SComboNo { get; set; }
        public int LCompID { get; set; }
        public string SLocationID { get; set; }
        public string SPricelist { get; set; }
        public int LCostType { get; set; }
        public double DUnitCost { get; set; }
        public int LDecimals { get; set; }
        public double DUnitWeight { get; set; }
        public int NTaxIncl { get; set; }
        public double DQtyIBT { get; set; }
        public string SPriceUnit { get; set; }
        public double DPriceUnitConv { get; set; }
        public string SShipUnit { get; set; }
        public double DShipUnitConv { get; set; }
        public double DNonStockCost { get; set; }
        public int LLineParent { get; set; }
        public int LLineChild { get; set; }
        public double DQtySupplied { get; set; }
        public int LSupplyAction { get; set; }
        public double DPriceUnitPrice { get; set; }
        public int NDeleted { get; set; }
        public double DQtySuppliedInv { get; set; }
        public double DValueA { get; set; }
        public double DValueB { get; set; }
        public double DValueC { get; set; }
        public double DValueD { get; set; }
        public string STaxCode { get; set; }
        public string SPromoID { get; set; }
        public string SPromoDescr { get; set; }
        public int LAdditionalNumber01 { get; set; }
        public int NPriceByWeight { get; set; }
        public int DTotalWeight { get; set; }
        public int GINSource { get; set; } = 1;
        public string GINStatus { get; set; } = "Pending";
        public string GINModifyBy { get; set; } = "";
        public DateTime GINModifyDate { get; set; } = DateTime.Now;
    }
}
