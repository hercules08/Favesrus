using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favesrus.Model.Entity
{
    public class FavesrusRole : IdentityRole
    {
        public FavesrusRole() : base() { }
        public FavesrusRole(string name) : base(name) { }
    }
}
