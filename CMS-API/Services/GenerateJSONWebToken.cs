using Microsoft.Extensions.Configuration;
using System;
using System.Text;

namespace CMS_API.Services
{
    public class GenerateJSONWebToken
    {
        //public string GenerateTokken(IConfiguration _config)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(
        //      issuer: _config["Jwt:Issuer"],
        //      audience: _config["Jwt:Issuer"],
        //      claims: null,
        //      notBefore: DateTime.UtcNow,
        //      expires: DateTime.Now.AddMinutes(20),
        //      signingCredentials: credentials);
        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}
    }
}