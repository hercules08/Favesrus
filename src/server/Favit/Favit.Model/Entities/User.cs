using Favit.Model.Base;
using System;
using System.Collections.Generic;

namespace Favit.Model.Entities
{
    public partial class User : EntityBase
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual Nullable<System.DateTime> Birthday { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string Gender { get; set; }
        public virtual string ModoId { get; set; }
        public virtual Nullable<DateTime> ModoMembershipDate { get; set; }
        public virtual bool CardOnFile { get; set; }
        public virtual ICollection<User> FavFriends { get; set; }
        public virtual ICollection<Item> FavItems { get; set; }

        public string Pic { get; set; }
    }
}
