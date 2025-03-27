using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain_Layer.Models
{
    public class CropsModel
    {

        public int Id { get; set; }
        public string FMTITEMNO { get; set; }
        public string ItemNo { get; set; }
        public string StockUnit { get; set; }
        public string Title { get; set; }
        public int CropCategoryId { get; set; }
        public int Status { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public CropCategoryModel CropCategory { get; set; } = new CropCategoryModel();
    }
}
