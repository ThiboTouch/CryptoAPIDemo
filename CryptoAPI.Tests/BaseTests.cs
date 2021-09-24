using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CryptoAPI.Tests
{
    public class BaseTests
    {
        protected CryptoAPIIdentityContext BuildContext(string databaseName)
        {
            var options = new DbContextOptionsBuilder<CryptoAPIIdentityContext>()
                .UseInMemoryDatabase(databaseName).Options;

            var dbContext = new CryptoAPIIdentityContext(options);
            return dbContext;
        }

        protected ControllerContext BuildControllerContextWithDefaultUser()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
               {
                new Claim(ClaimTypes.Name, "example@hotmail.com"),
                new Claim(ClaimTypes.Email, "example@hotmail.com"),
               }, "test"));

            return new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };
        }
    }
}
