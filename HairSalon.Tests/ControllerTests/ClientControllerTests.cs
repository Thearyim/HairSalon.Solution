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
        public void CreateClient_ReturnsCorrectView_True()
        {
            //Arrange
           ClientController controller = new ClientController();

            //Act
            ActionResult newView = controller.CreateClient("Bryan", 0);

            //Assert
            Assert.IsInstanceOfType(newView, typeof(RedirectToActionResult));
        }

        [TestMethod]
        public void DeleteClient_ReturnsCorrectView_True()
        {
            //Arrange
            ClientController controller = new ClientController();

            //Act
            ActionResult newView = controller.Delete(0);

            //Assert
            Assert.IsInstanceOfType(newView, typeof(ViewResult));
        }

        [TestMethod]
        public void ShowClient_ReturnsCorrectView_True()
        {
            //Arrange
            ClientController controller = new ClientController();

            //Act
            ActionResult newView = controller.Show(0);

            //Assert
            Assert.IsInstanceOfType(newView, typeof(ViewResult));
        }
    }
}