using Project_G.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_G.Server.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> Users { get; }
        User GetUser(int id);
        void SaveUser(int id);
    }
}