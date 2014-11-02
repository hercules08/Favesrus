using ProjectG.Server.Interfaces;
using ProjectG.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjectG.Server.BLL.Repos
{
    public class UserRepository:IUserRepository
    {
        private ProjectGDBContext context = new ProjectGDBContext();

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