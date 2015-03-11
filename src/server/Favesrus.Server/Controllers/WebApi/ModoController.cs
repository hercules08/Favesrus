using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Favesrus.Server.Controllers.WebApi
{

    public class ModoController : ApiController
    {


        public async Task<IHttpActionResult> GetModoToken()
        {
            return Ok();
        }

    }
}
