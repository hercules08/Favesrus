using Favesrus.DAL;
using Favesrus.Domain.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Favesrus.Services
{
    public class FavesrusRoleManager : RoleManager<FavesrusRole>
    {
        public FavesrusRoleManager(RoleStore<FavesrusRole> store) : base(store) { }

        public static FavesrusRoleManager Create(
                IdentityFactoryOptions<FavesrusRoleManager> options,
            IOwinContext context)
        {
            return new FavesrusRoleManager(new RoleStore<FavesrusRole>(context.Get<FavesrusDbContext>()));
        }
    }
}

