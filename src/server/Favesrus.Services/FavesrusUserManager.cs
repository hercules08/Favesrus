using Favesrus.DAL.Impl;
using Favesrus.Model.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Favesrus.Services
{
    public class FavesrusUserManager : UserManager<FavesrusUser>
    {
        public FavesrusUserManager(IUserStore<FavesrusUser> store)
            : base(store) { }

        public static FavesrusUserManager Create(
            IdentityFactoryOptions<FavesrusUserManager> options,
            IOwinContext context)
        {
            FavesrusDbContext dbContext = context.Get<FavesrusDbContext>();
            FavesrusUserManager manager =
                new FavesrusUserManager(new UserStore<FavesrusUser>(dbContext));
            return manager;
        }
    }
}
