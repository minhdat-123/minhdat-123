using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DemoAuthentication.Web.Controllers
{
    [Authorize]
    [RoutePrefix("api/data")]
    public class DataController : ApiController
    {
        [HttpGet]
        [Route("protected")]
        public IHttpActionResult GetProtectedData()
        {
            var username = User.Identity.Name;
            return Ok(new string[] { username });
            // Fetch and return protected data based on the authenticated user
            // ...
        }
    }
}
