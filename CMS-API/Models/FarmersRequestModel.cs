using Domain_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS_API.Models
{
    public class FarmersRequestModel
    {
        public List<FarmerModel> Results { get; set; } = new List<FarmerModel>();
    }
}
