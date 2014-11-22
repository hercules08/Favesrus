using System;
using System.Collections.Generic;

namespace Favit.Server.Models
{
    public partial class User
    {
        public User()
        {
            this.Favs = new List<User>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ModoId { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public virtual ICollection<User> Favs { get; set; }
        public virtual ICollection<Item> FavItems { get; set; }

        public string Pic { get; set; }
    }
}
