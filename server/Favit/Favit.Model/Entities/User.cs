using Favit.Model.Base;
using System;
using System.Collections.Generic;

namespace Favit.Model.Entities
{
    public partial class User : EntityBase
    {
        public User()
        {
            this.Favs = new List<User>();
            this.FavItems = new List<Item>();
        }

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
