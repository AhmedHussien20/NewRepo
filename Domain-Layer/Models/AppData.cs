using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain_Layer.Models
{
    public class AppDataModel
    {
        public int Id { get; set; }
        public String Link { get; set; }
        public String AppVersion { get; set; }
        public String ChangeLog { get; set; }
        public IFormFile UploadFile { get; set; }
    }
}
