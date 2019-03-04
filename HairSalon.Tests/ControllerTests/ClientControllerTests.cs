using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;

namespace HairSalon.Tests
{
    [TestClass]
    public class ClientControllerTest
    {
        [TestMethod]
        public void New_ReturnsCorrectView_True()
        {
            //Arrange
           ClientController controller = new ClientController();

            //Act
            ActionResult newView = controller.NewClient(stylistId: 1);

            //Assert
            Assert.IsInstanceOfType(newView, typeof(ViewResult));
        }

        [TestMethod]
        public void Delete_ReturnsCorrectView_True()
        {
            //Arrange
            ClientController controller = new ClientController();

            //Act
            ActionResult newView = controller.DeleteClient(stylistId: 1, clientId: 100);

            //Assert
            Assert.IsInstanceOfType(newView, typeof(ViewResult));
        }

        [TestMethod]
        public void Show_ReturnsCorrectView_True()
        {
            //Arrange
            ClientController controller = new ClientController();

            //Act
            ActionResult newView = controller.ShowClients(stylistId: 1, clientId: 100);

            //Assert
            Assert.IsInstanceOfType(newView, typeof(ViewResult));
        }

        [TestMethod]
        public void Show_ReturnsExpectedModelInView()
        {
            //Arrange
            ClientController controller = new ClientController();

            //Act
            ActionResult newView = controller.ShowClients(stylistId: 1, clientId: 100);

            //Assert
            ViewResult view = newView as ViewResult;
            Assert.IsNotNull(view);
            Assert.IsNotNull(view.Model);

            Dictionary<string, object> actualModel = view.Model as Dictionary<string, object>;
            Assert.IsNotNull(actualModel);

            //ClientClass expectedClient = actualModel["client"] as ClientClass;
            //StylistClass expectedStylish = actualModel["stylish"] as StylistClass;
            //Assert.IsNotNull(expectedClient);
            //Assert.IsNotNull(expectedStylish);

            //Assert.IsTrue(expectedClient.GetName() == "Bryan");
            //Assert.IsTrue(expectedStylish.GetName() == "Sophie");
        }
    }
}