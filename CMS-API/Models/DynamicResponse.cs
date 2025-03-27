using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS_API.Models
{
    public class DynamicResponse
    {
        public string errorMessage { get; set; }
        public bool error { get; set; }
        public int TotalRows { get; set; }
        public int TotalPage { get; set; }
        public dynamic results { get; set; }
    }
}
