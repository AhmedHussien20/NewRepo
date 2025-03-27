using System;
using System.Collections.Generic;
using System.Text;

namespace Domain_Layer.Models
{
    public class GINModel
    {
        public string CustomerNumber { get; set; }
        public DateTime EntryDate { get; set; }
        public int OrderType { get; set; }
        public int OrderID { get; set; }
        public string CustomerID { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipStateOrProvince { get; set; }
        public string ShipCountry { get; set; }
        public string ShipPhoneNumber { get; set; }
        public string ShipContact { get; set; }
        public int Printed { get; set; }
        public string Notes { get; set; }
        public string SShipEmail { get; set; }
        public int NPosted { get; set; }
        public string SLocationID { get; set; }
        public string STranNo { get; set; }
        public string DTranNo { get; set; }
        public int NComplete { get; set; }
        public string SLinkedTranNo { get; set; }
        public string STerminal { get; set; }
        public string SBillAddr { get; set; }
        public string SBillCity { get; set; }
        public string SBillProvince { get; set; }
        public string SBillCountry { get; set; }
        public string SBillPhoneNo { get; set; }
        public string SBillEmail { get; set; }
        public string SBillContact { get; set; }
        public int NOnHold { get; set; }
        public string DCustOpenBalance { get; set; }
        public string SIDNo { get; set; }
        public string STransporter { get; set; }
        public string SVehicleNumber { get; set; }
        public string Depot { get; set; }
        public string DOPreparedBy { get; set; }
        public int GINSource { get; set; } = 1;
        public string GINStatus { get; set; } = "Pending";
        public string GINModifyBy { get; set; }
        public string GINModifyDate { get; set; }
        public string ItemNum { get; set; }
        public string ItemDescr { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }
        public string UnitType { get; set; }
        public double LineTotal { get; set; }
        public double LineVat { get; set; }
        public double SQtyOrdered { get; set; }
        public double DQtyBO { get; set; }
        public int LAdditionalNumber01 { get; set; }
        public int GINType { get; set; }
        public string GinBagType { get; set; }
        public string GINTransactionType { get; set; }
        public string GINWeightSource { get; set; }

        public double TotalQuantity { get; set; }

        /* public int OrderType { get; set; }
        public string OrderIDPrefix { get; set; }
        public string UserName { get; set; }
        public string SalesrepCode { get; set; }
        public DateTime OrderDate { get; set; }
        public string ShipPostalCode { get; set; }
        public DateTime ShipDate { get; set; }
        public string ShipFaxNumber { get; set; }
        public DateTime ExpDate { get; set; }
        public string OrderStatus { get; set; }
        public double OrderTotalExcl { get; set; }
        public double OrderVatTotal { get; set; }
        public string LinkDoc { get; set; }
        public double DTotalDiscount { get; set; }
        public int LLinkedType { get; set; }
        public int DRounding { get; set; }
        public int LPostseq { get; set; }
        public string SBranchDocID { get; set; }
        public int LUniq { get; set; }
        public int LCompID { get; set; }
        public string SConsOrdNo { get; set; }
        public string SConsInvNo { get; set; }
        public int NDownloaded { get; set; }
        public string STaxRegNo { get; set; }
        public string SIntRef01 { get; set; }
        public string SIntRef02 { get; set; }
        public int NLayBye { get; set; }
        public int LLayByeTerm { get; set; }
        public int NLaybyPeriodType { get; set; }
        public string SLoyaltyNo { get; set; }
        public double DLoyaltyPointsOpen { get; set; }
        public double DLoyaltyPointsEarn { get; set; }
        public double DLoyaltyValueOpen { get; set; }
        public double DLoyaltyValueEarn { get; set; }
        public int LLinkedComID { get; set; }
        public string SBillPostalCode { get; set; }
        public string SBillFaxNo { get; set; }
        public string SBillCellNo { get; set; }
        public string SBillTerms { get; set; }
        public string SShipVia { get; set; }
        public string SCurrSource { get; set; }
        public string SCurrFunc { get; set; }
        public int DCurrRate { get; set; }
        public string SCurrType { get; set; }
        public int NCurrOper { get; set; }
        public string SRefNo { get; set; }
        public int NTemplate { get; set; }
        public int NAllSupplied { get; set; }
        public int NSubType { get; set; }
        public string SExtLoyaltyNo { get; set; }
        public string SExtLoyaltyID { get; set; }
        public string SShiptoCode { get; set; }
        public int LShipCompID { get; set; }
        public string SSuspendRef { get; set; }
        public string SOptHdr { get; set; }
        public int IPaidERP { get; set; }
        public string SLinkedQuote { get; set; } */
    }
}
