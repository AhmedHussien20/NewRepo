using System;
using System.Collections.Generic;
using System.Text;

namespace Domain_Layer.Models
{
    public class PaginationData
    {
        public int TotalRows { get; set; }
        public int TotalPage { get; set; }
        public int rowCount { get; set; }
        public string tablename { get; set; }
        public string Column { get; set; }
        public string Id { get; set; }
    }
}
