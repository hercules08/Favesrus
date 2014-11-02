using Project_G.Server.Interfaces;
using Project_G.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_G.Server.BLL.Repos
{
    public class UserRepository:IUserRepository
    {
        private ProjectG_DBContext context = new ProjectG_DBContext();

        public IEnumerable<User> Users
        {
            get { return context.Users; }
        }

        public User GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveUser(int id)
        {
            throw new NotImplementedException();
        }
    }
}