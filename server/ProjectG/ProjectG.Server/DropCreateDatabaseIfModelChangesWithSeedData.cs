using ProjectG.Server.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProjectG.Server
{
    public class DropCreateDatabaseIfModelChangesWithSeedData : 
        DropCreateDatabaseIfModelChanges<ProjectGDBContext>
    {

        protected override void Seed(ProjectGDBContext context)
        {

            User[] users = 
            {
                new User() {
                    FirstName = "Damola",
                    LastName ="Omotosho",
                    Email = "damola.omotosho@gmail.com",
                    Birthday = new DateTime(1987, 2, 8)
                }
            };

            foreach(var user in users)
            {
                context.Users.Add(user);
            }

            base.Seed(context);
        }
    }
}