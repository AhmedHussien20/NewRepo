using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application_Layer.Interfaces;
using Crop_Management_System.Services;
using Domain_Layer.Enums;
using Domain_Layer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Crop_Management_System.Pages.Views.Authorisation
{
    public class passwordResetModel : PageModel
    {
        private readonly IDataService _db;
        private readonly IConfiguration _config;

        private UsersModel UsersModel = new UsersModel();
        private CookieHandler _cookieHandler;
        private EmailHandler _emailHandler = new EmailHandler();

        [BindProperty(SupportsGet = true)]
        public bool hasError { get; set; } = false;
        [BindProperty(SupportsGet = true)]
        public bool accountReset { get; set; } = false;
        [BindProperty(SupportsGet = true)]
        public string key { get; set; }
        [BindProperty(SupportsGet = true)]
        public string errorMessage { get; set; }
        [BindProperty(SupportsGet = true)]
        public bool expiredCookie { get; set; } = false;
        [BindProperty(SupportsGet = true)]
        public string message { get; set; }
        [BindProperty(SupportsGet = true)]
        public string email { get; set; }
        [BindProperty(SupportsGet = true)]
        public int attempts { get; set; } = 3;

        public passwordResetModel(IDataService db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        public async Task<IActionResult> OnPostAsync(String email, String defaultPasswoord, String newPassword, String confirmNewPassword)
        {
            try
            {
                //Send Reset Code
                if (accountReset && key == null)
                {
                    return await sendResetCode(email);
                }

                //Reset Account Password
                else if (accountReset && key != null)
                {
                    return await resetAccountPassword(newPassword, confirmNewPassword);
                }

                //Change Reset Password
                else
                {
                    _cookieHandler = new CookieHandler(Response);
                    email = _cookieHandler.getCookie(CookieValues.userEmail, Request);
                    return await resetDefaultPassword(email, defaultPasswoord, newPassword, confirmNewPassword);
                }
            }
            catch (Exception e)
            {
                return RedirectToPage(new
                {
                    hasError = true,
                    message = e.Message,
                    hasResponse = true
                });
            }
        }

        private async Task<IActionResult> sendResetCode(string email)
        {
            //check if exist
            _cookieHandler = new CookieHandler(Response);
            var isExist = await _db.UsersGetByEmail(email);
            if (isExist != null)
            {
                await _emailHandler.passwordResetEmailAsync(context: HttpContext,
                                                      _config: _config,
                                                      email: isExist.Email,
                                                      key: isExist.SecurityStamp);
                return RedirectToPage(new
                {
                    accountReset = true,
                    hasError = true,
                    message = $"Account Reset Link Sent To {email}"
                });
            }
            else
            {
                return RedirectToPage(new
                {
                    accountReset = true,
                    hasError = true,
                    message = $"Account does not exist"
                });
            }
        }

        private async Task<IActionResult> resetAccountPassword(string newPassword, string confirmNewPassword)
        {
            if (newPassword != confirmNewPassword)
            {
                return RedirectToPage(new
                {
                    accountReset = true,
                    key = key,
                    hasError = true,
                    message = $"Confirmed Password does not match"
                });
            }

            ResetPasswordModel users = new ResetPasswordModel()
            {
                defaultPasswoord = key.Trim(),
                newPassword = newPassword
            };

            await _db.UsersResetPassword(users, key);
            return RedirectToPage("/Login", new
            { });
        }

        private async Task<IActionResult> resetDefaultPassword(string email, string defaultPasswoord, string newPassword, string confirmNewPassword)
        {
            _cookieHandler = new CookieHandler(Response);

            //check if match
            if (newPassword != confirmNewPassword)
            {
                return RedirectToPage(new
                {
                    email = email,
                    hasError = true,
                    message = $"Confirmed Password does not match"
                });
            }

            if (newPassword == defaultPasswoord)
            {
                return RedirectToPage(new
                {
                    email = email,
                    hasError = true,
                    message = $"New password cannot be the same as old password"
                });
            }

            //check if password is valid
            UsersModel users = new UsersModel()
            {
                Email = email,
                Password = defaultPasswoord
            };

            var isValid = await _db.UsersPortalLogin(users);
            if (isValid == null)
            {
                var attemptsLeft = attempts - 1;
                return RedirectToPage(new
                {
                    email = email,
                    hasError = true,
                    message = $"Incorrect default password entered.\n\nAttempts Left: {attemptsLeft}"
                });
            }
            else
            {
                ResetPasswordModel model = new ResetPasswordModel()
                {
                    defaultPasswoord = defaultPasswoord,
                    newPassword = newPassword,
                    userId = int.Parse(_cookieHandler.getCookie(CookieValues.userId, Request))
                };

                await _db.UsersResetPassword(model, null);

                _cookieHandler.Set(CookieValues.userId.ToString(), isValid.Id.ToString(), 10);
                _cookieHandler.Set(CookieValues.userName.ToString(), isValid.FirstName + ' ' + isValid.LastName, 10);
                _cookieHandler.Set(CookieValues.userEmail.ToString(), isValid.Email, 10);
                _cookieHandler.Set(CookieValues.userRole.ToString(), isValid.RolesModel.Title, 10);

                return RedirectToPage("/Login", new
                { });
            }
        }
    }
}
