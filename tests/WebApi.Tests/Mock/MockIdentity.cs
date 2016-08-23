using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNet.Identity;

namespace WebApi.Tests.Mock
{
    public class MockIdentity
    {
        public const string Username = "ouruser";

        public static IIdentity Create()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, Username),
                new Claim(ClaimTypes.Role, "role")
            };
            return new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie, ClaimTypes.Name, ClaimTypes.Role);
        }
    }
}