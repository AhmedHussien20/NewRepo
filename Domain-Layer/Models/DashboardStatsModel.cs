using System;
using System.Collections.Generic;
using System.Text;

namespace Domain_Layer.Models
{
    public class DashboardStatsModel
    {
        public int TransactionsCount { get; set; }
        public int FarmerCount { get; set; }
        public int TotalBags { get; set; }

        public float Revenue { get; set; }
    }
}
