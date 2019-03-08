using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Tests
{
    [TestClass]
    public class ClientTest
    {
        [TestMethod]
        public void GetName_ReturnsName_String()
        {
            //Arrange
            ClientClass client = new ClientClass("Bryan", 1);

            //Act
            var newName = client.GetName();

            //Assert
            Assert.IsInstanceOfType(newName, typeof(string));
        }

        [TestMethod]
        public void GetId_ReturnsExpectedId()
        {
            //Arrange
            ClientClass client = new ClientClass(3, "Bryan", 1);

            //Act
            int id = client.GetId();

            //Assert
            Assert.AreEqual(3, id);
        }

        [TestMethod]
        public void GetStylistId_ReturnsExpectedId_ClientInt()
        {
            //Arrange
            ClientClass client = new ClientClass("Bryan", 1);

            //Act
            int stylishId = client.GetStylistId();

            //Assert
            Assert.AreEqual(1, stylishId);
        }

        [TestMethod]
        public void GetAll_ReturnsAllClientRecordsInTheDatabase()
        {
            //Arrange
            ClientClass anyClient = new ClientClass("Client1", 2);

            //Act
            anyClient.Save();

            //Act
            List<ClientClass> allClients = ClientClass.GetAll();

            //Assert
            Assert.IsTrue(allClients.Count >= 1);
        }

        [TestMethod]
        public void Save_SavesToDatabase_ClientList()
        {
            //Arrange
            ClientClass newClient = new ClientClass("Client2", 2);

            //Act
            newClient.Save();
            List<ClientClass> allClients = ClientClass.GetAll();

            bool clientFound = false;
            foreach (ClientClass actualClient in allClients)
            {
                if (actualClient.GetName() == newClient.GetName() 
                    && actualClient.GetStylistId() == newClient.GetStylistId())
                {
                    clientFound = true;
                    break;
                }
            }

            //Assert
            Assert.IsTrue(clientFound);
        }

        [TestMethod]
        public void Find_ReturnsClientWithTheMatchingId()
        {
            //Arrange
            string clientName = "Client3";
            int stylistId = 1;
            ClientClass expectedClient = new ClientClass(clientName, stylistId);

            //Act
            expectedClient.Save();
            ClientClass actualClient = ClientClass.Find(expectedClient.GetId());

            // Assert
            Assert.IsNotNull(actualClient);
            Assert.IsTrue(expectedClient.GetId() == actualClient.GetId());
            Assert.IsTrue(expectedClient.GetName() == actualClient.GetName());
        }

        [TestMethod]
        public void Delete_RemovesTheClientFromTheDatabase()
        {
            //Arrange
            string clientName = "Client4";
            int stylistId = 1;
            ClientClass expectedClient = new ClientClass(clientName, stylistId);

            //Act
            expectedClient.Save();
            expectedClient.Delete(expectedClient.GetId());
            ClientClass deletedClient = ClientClass.Find(expectedClient.GetId());

            // Assert
            Assert.IsNull(deletedClient);
        }
    }
}
