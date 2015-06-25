using Favesrus.DAL;
using Favesrus.Domain.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Favesrus.IntegrationTest.DAL
{
    [TestClass]
    public class DALTest
    {
        [TestMethod]
        public void Able_To_Get_GiftItems_List_Using_FavesrusSeeder()
        {
            // Arrange
            List<GiftItem> list = new List<GiftItem>();

            // Act
            list = FavesrusSeeder.GetGiftItems();

            // Assert
            Assert.AreNotEqual(list.Count, 0);
        }
    }
}
