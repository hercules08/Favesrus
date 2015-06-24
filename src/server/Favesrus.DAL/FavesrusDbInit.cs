using Favesrus.Domain.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;

namespace Favesrus.DAL
{
    public class FavesrusDbInit :
        DropCreateDatabaseIfModelChanges<FavesrusDbContext>
    {
        public FavesrusDbInit(bool seedMe = true)
        {
            _seedMe = seedMe;
        }

        bool _seedMe;

        protected override void Seed(FavesrusDbContext context)
        {

            UserManager<FavesrusUser> userMgr =
                new UserManager<FavesrusUser>(new UserStore<FavesrusUser>(context));
            RoleManager<FavesrusRole> roleMgr = new RoleManager<FavesrusRole>(new RoleStore<FavesrusRole>(context));

            string roleName = "Admin";
            string userName = "Admin";
            string password = "favesrus1";
            string email = "favesrus@gmail.com";

            // Check if role exists and create if not
            if (!roleMgr.RoleExists(roleName))
                roleMgr.Create(new FavesrusRole(roleName));

            // Check if role exists and create if not
            if (!roleMgr.RoleExists("Customer"))
                roleMgr.Create(new FavesrusRole("Customer"));

            // Check if user exists and create if not
            FavesrusUser user = userMgr.FindByName(userName);
            if (user == null)
            {
                userMgr.Create(new FavesrusUser
                {
                    UserName = userName,
                    Email = email,
                    WishLists = new List<WishList>() { new WishList() { WishListName = "Default" } }
                }, password);
                user = userMgr.FindByName(userName);
            }

            // Check if user is in role and add if not
            if (!userMgr.IsInRole(user.Id, roleName))
                userMgr.AddToRole(user.Id, roleName);

            if (_seedMe)
            {
                FavesrusSeeder.Seed(context);
            }

            base.Seed(context);
        }
    }
}
