using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestAuthentication.Data.Identity;
using TestAuthentication.Data.Models;

namespace TestAuthentication.Controllers
{
    [Produces("application/json")]
    [AllowAnonymous]
    public class DefaultController : Controller
    {

        public DefaultController(ApplicationUserManager userManager)
        {

        }

        /// <summary>
        /// Get a 200 OK from the server
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/test")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult TestMethod()
        {
            return Ok("Welcome");
        }
    }
}