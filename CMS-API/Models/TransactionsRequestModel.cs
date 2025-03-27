using Domain_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS_API.Models
{
    public class TransactionsRequestModel
    {
        public List<TransactionsModel> Results { get; set; } = new List<TransactionsModel>();
    }
}
