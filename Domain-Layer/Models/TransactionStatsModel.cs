using System;
using System.Collections.Generic;
using System.Text;

namespace Domain_Layer.Models
{
    public class TransactionStatsModel
    {
        public int TransactionsCount { get; set; }
        public int PendingTransactrions { get; set; }
        public int ApprovedTransactrions { get; set; }
        public float Revenue { get; set; }

    }
}
