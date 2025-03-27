using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Crop_Management_System.Pages.Views.Farmers
{
    public class FarmerCreationModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int userId { get; set; }
        public bool updateData { get; set; } = false;

        public void OnGet()
        {
            if (userId > 0)
            {
                updateData = true;
            }
            else
            {
                updateData = false;
            }
        }
    }
}
