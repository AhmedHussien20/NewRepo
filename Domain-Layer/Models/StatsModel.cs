using System;
using System.Collections.Generic;
using System.Text;

namespace Domain_Layer.Models
{
    public static class StatsModel
    {
        public class StatsFilterModel
        {
            public String provinceId { get; set; }
            public String districtId { get; set; }
            public String satelliteId { get; set; }
            public String cropId { get; set; }
            public DateTime startDateFilter { get; set; } = new DateTime(DateTime.Now.Year, 1, 1);
            public DateTime endDateFilter { get; set; } = new DateTime(DateTime.Now.Year, 12, 31);
            public bool shouldFilter { get; set; }
        }

        public class StatsByYear
        {
            public double Total { get; set; }
            public int Month { get; set; }
            public string Year { get; set; }
        }

        public class StatsByWeek
        {
            public double Total { get; set; }
            public int Month { get; set; }
            public string Week { get; set; }
        }

        public class StatsBySatelietSales
        {
            public double Total { get; set; }
            public string Name { get; set; }
        }

        public class StatsByByCropsSold
        {
            public double Total { get; set; }
            public string Title { get; set; }
        }
    }
}
