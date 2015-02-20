using Favesrus.Model.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Favesrus.DAL.Impl
{
    public class FavesrusDbInit:
        DropCreateDatabaseIfModelChanges<FavesrusDbContext>
    {      
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

            // Check if user exits and create if not
            FavesrusUser user = userMgr.FindByName(userName);
            if (user == null)
            {
                userMgr.Create(new FavesrusUser
                {
                    UserName = userName,
                    Email = email
                }, password);
                user = userMgr.FindByName(userName);
            }

            // Check if user is in role and add if not
            if (!userMgr.IsInRole(user.Id, roleName))
                userMgr.AddToRole(user.Id, roleName);
            
            base.Seed(context);
        }
    }
}
