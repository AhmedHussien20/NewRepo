using Domain_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS_API.Models
{
    public class KYCRequestModel
    {
        public FarmerKycModel Results { get; set; } = new FarmerKycModel();
    }
}
