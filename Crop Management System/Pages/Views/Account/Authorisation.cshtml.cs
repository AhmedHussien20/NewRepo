using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application_Layer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Crop_Management_System.Pages.Views.Account
{
    public class AuthorisationModel : PageModel
    {
        private readonly IDataService _db;
        public bool hasError = false;
        public string message;

        [BindProperty(SupportsGet = true)]
        public string key { get; set; }

        public AuthorisationModel(IDataService db)
        {
            _db = db;
        }

        public async Task OnGetAsync()
        {
            //check if security key is present
            if (key == null)
            {
                hasError = true;
                message = "Invalid Request, Missing Security Key";
            }
            else
            {
                //Validate security key
                var isValid = await _db.ActivateAccount(key);
                if (isValid)
                {
                    hasError = false;
                    message = "Valid Request, Account Activated";
                }
                else
                {
                    hasError = true;
                    message = "Invalid Request, Security Key does not exist";
                }
            }
        }
    }
}
