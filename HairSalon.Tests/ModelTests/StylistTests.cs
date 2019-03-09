using System.Collections.Generic;
using System.Linq;
using HairSalon.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HairSalon.Tests
{
    [TestClass]
    public class StylistTests
    {
        [ClassInitialize]
        public static void InitializeTests(TestContext context)
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=hairsalon_test;";
        }

        [TestMethod]
        public void GetName_ReturnsTheExpectedStylistName()
        {
            //Arrange
            StylistClass stylist = new StylistClass("Stylist1");

            //Act
            string newName = stylist.GetName();

            //Assert
            Assert.IsInstanceOfType(newName, typeof(string));
        }

        [TestMethod]
        public void GetId_ReturnsTheExpectedStylistId()
        {
            //Arrange
            StylistClass stylist = new StylistClass(1, "Stylist2");

            //Act
            int stylishId = stylist.GetId();

            //Assert
            Assert.AreEqual(1, stylishId);
        }

        [TestMethod]
        public void GetSpecialties_ReturnsTheExpectedSetOfSpecialties()
        {
            //Arrange
            List<SpecialtyClass> expectedSpecialties = new List<SpecialtyClass>
            {
                new SpecialtyClass("Specialty1"),
                new SpecialtyClass("Specialty2")
            };

            StylistClass stylist = new StylistClass("Stylist2", expectedSpecialties);

            //Act
            List<SpecialtyClass> actualSpecialties = stylist.GetSpecialties();

            //Assert
            Assert.IsTrue(expectedSpecialties.Count == actualSpecialties.Count);
            CollectionAssert.AreEqual(expectedSpecialties, actualSpecialties);
        }

        [TestMethod]
        public void ClearAll_RemovesAllStylistsFromTheDatabase()
        {
            //Act
            StylistClass.DeleteAll();
            List<StylistClass> allStylists = StylistClass.GetAll();

            //Assert
            Assert.IsTrue(allStylists.Count == 0);
        }

        [TestMethod]
        public void GetAll_ReturnsAllStylistRecordsInTheDatabase()
        {
            //Arrange
            StylistClass anyStylist = new StylistClass("Stylist3");

            //Act
            anyStylist.Save();

            //Act
            List<StylistClass> allStylists = StylistClass.GetAll();

            //Assert
            Assert.IsTrue(allStylists.Count >= 1);
        }
        
        [TestMethod]
        public void Save_CreatesANewStylistRecordInTheDatabase()
        {
            //Arrange
            string stylistName = "Stylist4";
            StylistClass stylist = new StylistClass(stylistName);

            //Act
            stylist.Save();
            List<StylistClass> result = StylistClass.GetAll();

            //Assert
            Assert.IsTrue(result.Count > 0);
            Assert.IsTrue(result.Any(r => r.GetName() == stylistName));
        }

        [TestMethod]
        public void FindById_ReturnsTheExpectedStylistRecord()
        {
            //Arrange
            string stylistName = "Stylist5";
            StylistClass expectedStylist = new StylistClass(stylistName);

            //Act
            expectedStylist.Save();
            StylistClass actualStylist = StylistClass.Find(expectedStylist.GetId());

            // Assert
            Assert.IsNotNull(actualStylist);
        }

        [TestMethod]
        public void Delete_PermanentlyRemovesTheStylistRecordFromTheDatabase()
        {
            //Arrange
            string stylistName = "Stylist6";
            StylistClass expectedStylist = new StylistClass(stylistName);

            //Act
            expectedStylist.Save();
            StylistClass actualStylist = StylistClass.Find(expectedStylist.GetId());

            actualStylist.Delete(expectedStylist.GetId());

            // Assert
            StylistClass deletedStylist = StylistClass.Find(expectedStylist.GetId());
            Assert.IsNull(deletedStylist);
        }     
    }
}
