using System.Collections.Generic;
using System.Linq;
using HairSalon.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HairSalon.Tests
{
    [TestClass]
    public class StylishTest
    {
        [TestMethod]
        public void GetName_ReturnsName_StylistString()
        {
            //Arrange
            StylistClass stylist = new StylistClass("Stylist1", "Cut");

            //Act
            string newName = stylist.GetName();

            //Assert
            Assert.IsInstanceOfType(newName, typeof(string));
        }

        [TestMethod]
        public void GetId_ReturnsTheExpectedId_Int()
        {
            //Arrange
            StylistClass stylist = new StylistClass(1, "Stylist2", "Cut");

            //Act
            int stylishId = stylist.GetId();

            //Assert
            Assert.AreEqual(1, stylishId);
        }

        [TestMethod]
        public void GetAll_ReturnsEmptyListFromDatabase_StylistClassList()
        {
            //Arrange
            StylistClass anyStylist = new StylistClass("Stylist3", "Cut");

            //Act
            anyStylist.Save();

            //Act
            List<StylistClass> allStylists = StylistClass.GetAll();

            //Assert
            Assert.IsTrue(allStylists.Count >= 1);
        }
        
        [TestMethod]
        public void Save_SavesToDatabase_StylistList()
        {
            //Arrange
            string stylishName = "Stylist4";
            string stylishType = "Cut";
            StylistClass stylish = new StylistClass(stylishName, stylishType);

            //Act
            stylish.Save();
            List<StylistClass> result = StylistClass.GetAll();

            //Assert
            Assert.IsTrue(result.Count > 0);
            Assert.IsTrue(result.Any(r => r.GetName() == stylishName));
        }

        [TestMethod]
        public void FindById_ReturnsExpectedStylist_Stylist()
        {
            //Arrange
            string stylishName = "Stylist5";
            string stylishType = "Cut";
            StylistClass expectedStylish = new StylistClass(stylishName, stylishType);

            //Act
            expectedStylish.Save();
            StylistClass actualStylist = StylistClass.Find(expectedStylish.GetId());

            // Assert
            Assert.IsNotNull(actualStylist);
        }

        [TestMethod]
        public void Delete_DeletesStylistById_StylistList()
        {
            //Arrange
            string stylishName = "Stylist6";
            string stylishType = "Cut";
            StylistClass expectedStylish = new StylistClass(stylishName, stylishType);

            //Act
            expectedStylish.Save();
            StylistClass actualStylist = StylistClass.Find(expectedStylish.GetId());

            actualStylist.Delete(expectedStylish.GetId());

            // Assert
            StylistClass deletedStylist = StylistClass.Find(expectedStylish.GetId());
            Assert.IsNull(deletedStylist);
        }     
    }
}
