using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_G.Server.BLL.BusinessObjects
{
    public class UserBO
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ModoId { get; set; }
        public List<UserBO> Favorites { get; set; }
        public DateTime Birthday { get; set; }
    }
}