using DemoAuthentication.Web.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace DemoAuthentication.Web.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthenticationController : ApiController
    {
        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public IHttpActionResult Login(LoginModel model)
        {
            if (IsValidUser(model.Username, model.Password))
            {
                var token = GenerateToken(model.Username+model.Password);
                return Ok(new { token });
            }

            return Unauthorized();
        }

        private bool IsValidUser(string username, string password)
        {
            if (username == "demo" && password == "123")
            {
                return true;
            }
            return false;
        }

        private string GenerateToken(string username)
        {
            var issuer = "your-issuer";
            var audience = "your-audience";
            var secret = "ACDt1vR3lXToPQ1g3MyN";
            var expires = DateTime.UtcNow.AddMinutes(30);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
                Issuer = issuer,
                Audience = audience,
                Expires = expires,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                    SecurityAlgorithms.Aes128CbcHmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
