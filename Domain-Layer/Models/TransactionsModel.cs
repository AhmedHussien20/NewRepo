using System;
using System.Collections.Generic;
using System.Text;

namespace Domain_Layer.Models
{
    public class TransactionsModel
    {
        public int Id { get; set; }
        public string PRCN { get; set; }
        public string Location { get; set; }
        public int CropId { get; set; }
        public string FarmerNrc { get; set; }
        public int FarmerId { get; set; }
        public int DeviceId { get; set; }
        public string DeviceSerialNumber { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        public decimal RetractedAmount { get; set; }
        public string Status { get; set; }
        public string SerialNumber { get; set; }

        public DateTime Timestamp { get; set; }

        public CropsModel CropData { get; set; } = new CropsModel();
        public FarmerModel FarmerData { get; set; } = new FarmerModel();
        public DevicesModel DeviceData { get; set; } = new DevicesModel();
    }

    public class TransactionFilter
    {
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now;
        public bool shouldFilter { get; set; } = false;
        public bool dateFilter { get; set; } = false;
        public bool locationFilter { get; set; } = false;
        public string location { get; set; } = null;
        public int StartIndex { get; set; } = 0;
        public int BatchSize { get; set; } = 10;
        public string DeliveryOrderNo { get; set; } = null;
        public string status { get; set; } = null; 
    }
}
