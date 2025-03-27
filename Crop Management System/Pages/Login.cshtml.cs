using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrustructure_Layer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Crop_Management_System.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public bool error { get; set; }

        Encryption encryption = new Encryption();

        public void OnGet()
        {
            var data = encryption.Decrypt("vBsdbDmNEy561pSNXTebvpHSUeHWoHA1yQ+EhjHJkq0=");

            String name = "";
        }
    }
}
