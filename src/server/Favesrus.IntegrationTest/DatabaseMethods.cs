using Favesrus.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Favesrus.IntegrationTest
{
    [TestClass]
    public class DatabaseMethods
    {
        [TestMethod]
        public void CreateNewLocalDatabase()
        {
            using (var context  = new FavesrusDbContext())
            {
                try
                {
                    context.Database.Initialize(force: true);
                }
                catch(Exception ex)
                {
                    Assert.Fail(ex.Message);
                }
            }
        }
    }
}
