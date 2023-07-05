using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Extensions;

[assembly: OwinStartup(typeof(DemoAuthentication.Web.Startup))]

namespace DemoAuthentication.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);
            ConfigureWebApi(app);
        }

        private void ConfigureOAuth(IAppBuilder app)
        {
            var issuer = "your-issuer";
            var audience = "your-audience";
            var secret = "ACDt1vR3lXToPQ1g3MyN";

            var tokenOptions = new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }
            };

            app.UseJwtBearerAuthentication(tokenOptions);
        }

        private void ConfigureWebApi(IAppBuilder app)
        {
            // Configure Web API routing, etc.
            // ...
        }
    }
}