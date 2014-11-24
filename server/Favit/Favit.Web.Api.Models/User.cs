using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favit.Web.Api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ModoId { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public ICollection<User> Favs { get; set; }
        public ICollection<Item> FavItems { get; set; }

        public string Pic { get; set; }
    }
}
