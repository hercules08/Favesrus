using Favesrus.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Favesrus.IntegrationTest.Common
{
    [TestClass]
    public class CommonTest
    {
        [TestMethod]
        public void Constants_Contains_Deploy_String()
        {
            // Arrange
            string expected = "name=DeployContext";
            // Act
            string actual = Constants.DEPLOYED_CONTEXT;
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Constants_Contains_Conditional_Db_Name()
        {
            // Arrange
            string expected = "Favesrus_DEBUG";
            // Act
            string actual = Constants.DB_NAME;
            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
