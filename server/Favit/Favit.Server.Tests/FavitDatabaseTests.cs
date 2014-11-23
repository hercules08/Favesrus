using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using Favit.DAL;

namespace Favit.Server.IntegrationTests
{
    [TestClass]
    public class FavitDatabaseTests
    {
        [TestMethod]
        public void Can_Create_Database_With_Seed_Data_If_Model_Changes()
        {
            using (var context = new FavitDBContext())
            {
                try
                {
                    Database.SetInitializer<FavitDBContext>(new FavitInitializer());

                    context.Database.Initialize(force: true);
                }
                catch(Exception ex)
                {
                    Assert.Fail(ex.Message, ex);
                }
            }
        }
    }
}
