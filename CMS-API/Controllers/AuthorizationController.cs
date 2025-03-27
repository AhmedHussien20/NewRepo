using CMS_API.Models;
using CMS_API.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMS_API.Controllers
{
    [Authorize]
    public class AuthorizationController : BaseController
    {
        private IConfiguration _config;
        private GenerateJSONWebToken _generateJSONWebToken = new GenerateJSONWebToken();

        public AuthorizationController(IConfiguration config)
        {
            _config = config;
        }

        //[HttpGet]
        //[Route("api/authTest")]
        //public async Task<ActionResult<IEnumerable<string>>> GetAsync()
        //{
        //    var accessToken = await HttpContext.GetTokenAsync("access_token");
        //    var jwtToken = new JwtSecurityToken(accessToken);

        //    return new string[] {
        //        accessToken,
        //        $"Date: {DateTime.UtcNow}" ,
        //        $"Valid Till: {jwtToken.ValidTo.ToString()}"
        //    };
        //}

        //[AllowAnonymous]
        //[HttpPost]
        //[Route("api/authLogin")]
        //public IActionResult Login([FromBody] LoginModel data)
        //{
        //    IActionResult response = Unauthorized();
        //    var user = AuthenticateUser(data);
        //    if (user != null)
        //    {
        //        var tokenString = _generateJSONWebToken.GenerateTokken(_config);
        //        response = Ok(new
        //        {
        //            Token = tokenString,
        //            Error = false,
        //            nawa = "vsv",
        //            Message = "Success"
        //        });
        //    }
        //    else
        //    {
        //        response = BadRequest(new
        //        {
        //            Error = true,
        //            Message = "User Name Does Not Exist"
        //        });
        //    }
        //    return response;
        //}

        private LoginModel AuthenticateUser(LoginModel login)
        {
            LoginModel user = null;

            //Validate the User Credentials
            //Demo Purpose, I have Passed HardCoded User Information
            if (login.UserName == "nawa")
            {
                user = new LoginModel { UserName = "Jay", Password = "123456" };
            }
            return user;
        }
    }
}