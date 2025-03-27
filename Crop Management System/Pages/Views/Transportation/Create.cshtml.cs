using System;
using System.Threading.Tasks;
using Application_Layer.Interfaces;
using Crop_Management_System.Services;
using Infrustructure_Layer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Crop_Management_System.Pages.Views.Transporter
{
    public class CreateModel : PageModel
    {
        public async Task OnGetAsync() { }

        public class ResponseModel
        {
            public string errorMessage { get; set; }
            public bool error { get; set; }
        }
    }
}
