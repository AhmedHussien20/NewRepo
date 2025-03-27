using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application_Layer.Interfaces;
using Crop_Management_System.Services;
using Infrustructure_Layer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Crop_Management_System.Pages.Views.Admins
{
    public class AdminsCreationModel : PageModel
    {
        private EmailHandler _emailHandler = new EmailHandler();
        public Encryption _encryption = new Encryption();
        private readonly IConfiguration _config;
        private readonly IDataService _db;

        [BindProperty(SupportsGet = true)]
        public int userId { get; set; }
        public bool updateData { get; set; } = false;

        public AdminsCreationModel(IConfiguration _config, IDataService _db)
        {
            this._config = _config;
            this._db = _db;
        }

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

        public async Task<JsonResult> OnGetSendMailAsync(string email)
        {
            try
            {
                var accountData = await _db.UsersGetByEmail(email);
                if (accountData != null)
                {
                    await _emailHandler.sendRegistrationEmailAsync(context: HttpContext,
                                                           _config: _config,
                                                           email: accountData.Email,
                                                           password: _encryption.Decrypt(accountData.Password),
                                                           key: accountData.SecurityStamp);
                    ResponseModel response = new ResponseModel
                    {
                        error = false
                    };
                    return new JsonResult(response);
                }
                else
                {
                    ResponseModel response = new ResponseModel
                    {
                        errorMessage = "Account does not exist",
                        error = true
                    };
                    return new JsonResult(response);
                }
            }
            catch (Exception e)
            {
                ResponseModel response = new ResponseModel
                {
                    errorMessage = e.Message,
                    error = true
                };
                return new JsonResult(response);
            }
        }

        public class ResponseModel
        {
            public string errorMessage { get; set; }
            public bool error { get; set; }
        }
    }
}
