using Project_G.Server.Interfaces;
using Project_G.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Project_G.Server.Controllers
{
    public class UserController : ApiController
    {
        IUserRepository repo;

        public UserController(IUserRepository repo)
        {
            this.repo = repo;
        }

        // GET api/user
        public IEnumerable<User> Get()
        {
            return repo.Users;
        }

        // GET api/user/5
        public User Get(int id)
        {
            return repo.GetUser(id) ;
        }

        // POST api/user
        public void Post([FromBody]string value)
        {
        }

        // PUT api/user/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/user/5
        public void Delete(int id)
        {
        }
    }
}
