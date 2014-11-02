using ProjectG.Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ProjectG.Server.Controllers
{
    public class VoteController : ApiController
    {
        IRepository repo;
        public VoteController(IRepository repo)
        {
            this.repo = repo;
        }

        public void IncrementXBox()
        {

        }

        //public void IncrementPS4()
    }
}
