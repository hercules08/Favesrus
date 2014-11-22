using Favit.Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Favit.Server.Controllers
{
    public class VoteController : ApiController
    {
        IRepository repo;
        public VoteController(IRepository repo)
        {
            this.repo = repo;
        }

        [Route("api/vote/incrementxbox")]
        [HttpGet]
        public void IncrementXBox()
        {
            repo.IncrementXBoxCount();
        }

        [Route("api/vote/incrementps4")]
        [HttpGet]
        public void IncrementPS4()
        {
            repo.IncrementPS4Count();
        }
    }
}
