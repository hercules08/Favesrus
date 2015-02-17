using Favesrus.DAL.Impl;
using Favesrus.Model.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

