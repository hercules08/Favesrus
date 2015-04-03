using Favesrus.Model.Enum;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favesrus.Model.Entity
{
    public class FavesrusUser : IdentityUser
    {
        public FavesrusUser()
        {
            //Birthday = DateTime.Now;
        }

        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string ModoAccountId { get; set; }
        public virtual DateTime? Birthday { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual string ProfilePic { get; set; }
    }
}
