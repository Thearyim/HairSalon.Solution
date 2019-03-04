using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using HairSalon.Models;
using System.IO;
 
namespace HairSalon.Tests
{
    [TestClass]
    public class HairSalonTest : IDisposable
    {

        public void Dispose()
        {
            StylistClass.ClearAll();
            ClientClass.ClearAll();
        }

        public HairSalonTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8890;database=theary_im_test;";
        }

        [TestMethod]
        public void GetName_ReturnsName_StylistString()
        {
            //Arrange
            StylistClass Stylist = new StylistClass("Sophie");

            //Act
            var newName = Stylist.GetName();

            //Assert
            Assert.IsInstanceOfType(newName, typeof(string));
        }

        [TestMethod]
        public void GetId_ReturnsName_Int()
        {
            //Arrange
            StylistClass Stylist = new StylistClass("Sophie");

            //Act
            var newName = Stylist.GetId();

            //Assert
            Assert.IsInstanceOfType(newName, typeof(int));
        }

        [TestMethod]
        public void GetAll_ReturnsEmptyListFromDatabase_StylistClassList()
        {
            //Arrange
            List<StylistClass> newList = new List<StylistClass> { };

            //Act
            List<StylistClass> result = StylistClass.GetAll();

            //Assert
            CollectionAssert.AreEqual(newList, result);
        }
        
        [TestMethod]
        public void Save_SavesToDatabase_StylistList()
        {
            //Arrange
            string name = "Sophie";
            StylistClass Stylist = new StylistClass(name);

            //Act
            StylistClass.Save(name);
            List<StylistClass> result = StylistClass.GetAll();
            List<StylistClass> testList = new List<StylistClass>{Stylist};

            //Assert
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void FindById_ReturnsStylistList_Stylist()
        {
            //Arrange
            string name = "Sophie";

            //Act
            StylistClass.Save(name);
            StylistClass Stylist = StylistClass.FindById(1);

            StylistClass Bryan = new StylistClass("Sophie");
            Bryan = Stylist;

            //Assert
           Assert.IsInstanceOfType(Bryan, typeof(StylistClass));
        }

        [TestMethod]
        public void Delete_DeletesStylistById_StylistList()
        {
            //Arrange
            string name = "Sophie";
            List<StylistClass> StylistList = new List<StylistClass> {};

            //Act
            StylistClass.Save(name);
            List<StylistClass> StylistListTwo = StylistClass.GetAll();
            int id = StylistListTwo[0].GetId();
            StylistClass.Delete(id);
            List<StylistClass> StylistListThree = StylistClass.GetAll();

            //Assert
           CollectionAssert.AreEqual(StylistList, StylistListThree);
        }     
    }
}
