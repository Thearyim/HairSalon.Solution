using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Tests
{
    [TestClass]
    public class ClientTest : IDisposable
    {

        public void Dispose()
        {
            Client.ClearAll();
        }

        public HairSalonTest()
        {
            DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8890;database=theary_im_test;";
        }

        [TestMethod]
        public void GetName_ReturnsName_String()
        {
            //Arrange
            ClientClass Client = new ClientClass("Bryan", 1);

            //Act
            var newName = Client.GetName();

            //Assert
            Assert.IsInstanceOfType(newName, typeof(string));
        }

        [TestMethod]
        public void GetId_ReturnsName_ClientInt()
        {
            //Arrange
            ClientClass Client = new ClientClass("Bryan", 1);

            //Act
            var newName = Client.GetId();

            //Assert
            Assert.IsInstanceOfType(newName, typeof(int));
        }

        [TestMethod]
        public void GetStylistId_ReturnsName_ClientInt()
        {
            //Arrange
            ClientClass Client = new ClientClass("Bryan", 1);

            //Act
            var newName = Client.GetStylistId();

            //Assert
            Assert.IsInstanceOfType(newName, typeof(int));
        }

        [TestMethod]
        public void GetAll_ReturnsEmptyListFromDatabase_ClientClassList()
        {
            //Arrange
            List<ClientClass> newList = new List<ClientClass> { };

            //Act
            List<ClientClass> result = ClientClass.GetAll();

            //Assert
            CollectionAssert.AreEqual(newList, result);
        }

        [TestMethod]
        public void Save_SavesToDatabase_ClientList()
        {
            //Arrange
            ClientClass testStylist = new ClientClass("Bryan", 2);

            //Act
            testStylist.Save();
            List<ClientClass> result = ClientClass.GetAll();
            List<ClientClass> testList = new List<ClientClass>{testStylist};

            //Assert
            CollectionAssert.AreEqual(testList, result);
        }

        [TestMethod]
        public void GetClientsById_ReturnsClientList_Client()
        {
            //Arrange
            string name = "Bryan";

            //Act
            StylistClass.Save(name);
            StylistClass Client = StylistClass.FindById(1);
            int id = 0;
            id = Client.GetId();
            ClientClass ClientTwo = new ClientClass(name, id);
            ClientTwo.Save();
            int idTwo = ClientTwo.GetId();
            var tempList = ClientClass.GetClientById(idTwo);

            var Client = tempList;

            //Assert
            Assert.IsInstanceOfType(Client, typeof(ClientClass));
        }

        [TestMethod]
        public void DeleteClientsByStylistId_DeletesClientsByStylistId_ClientList()
        {
            //Arrange
            List<ClientClass> tempList = new List<ClientClass> {};
            string name = "Bryan";
            int id = 1;
            ClientClass Client = new ClientClass(name, id);

            //Act
            StylistClass.Save(name);
            Client.Save();
            ClientClass.DeleteClientsByStylistId(1);
            List<ClientClass> tempListThree = ClientClass.GetAll();

            //Assert
            CollectionAssert.AreEqual(tempList, tempListThree);
        }
    }
}
