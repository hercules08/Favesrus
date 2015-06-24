using Microsoft.AspNet.Identity.EntityFramework;

namespace Favesrus.Domain.Entity
{
    public class FavesrusRole : IdentityRole
    {
        public FavesrusRole() : base() { }
        public FavesrusRole(string name) : base(name) { }
    }
}
