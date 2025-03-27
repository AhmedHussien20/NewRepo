using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crop_Management_System.Pages
{
    public class UnauthorizedModel : PageModel
    {
        private readonly ILogger<UnauthorizedModel> _logger;

        public UnauthorizedModel(ILogger<UnauthorizedModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
