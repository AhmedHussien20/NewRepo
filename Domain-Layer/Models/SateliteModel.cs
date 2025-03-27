using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain_Layer.Models
{
    public class SateliteModel
    {
        public int Id { get; set; }
      
        public string Title { get; set; }
     
        public string Prefix { get; set; }
   
        public int DistrictId { get; set; }
        public DistrictModel DistrictData { get; set; }
    }
}
