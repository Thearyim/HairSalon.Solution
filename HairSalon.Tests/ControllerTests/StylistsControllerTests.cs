using HairSalon.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HairSalon.Tests
{
    [TestClass]
    public class StylistsControllerTest
    {
        [TestMethod]
        public void Index_ReturnsCorrectView_True()
        {
            //Arrange
           StylistsController controller = new StylistsController();

            //Act
            ActionResult indexView = controller.Index();

            //Assert
            Assert.IsInstanceOfType(indexView, typeof(ViewResult));
        }

        [TestMethod]
        public void New_ReturnsCorrectView_True()
        {
            //Arrange
           StylistsController controller = new StylistsController();

            //Act
            ActionResult newView = controller.NewStylist();

            //Assert
            Assert.IsInstanceOfType(newView, typeof(ViewResult));
        }

        [TestMethod]
        public void Show_ReturnsCorrectView_True()
        {
            //Arrange
           StylistsController controller = new StylistsController();

            //Act
            ActionResult newView = controller.ShowStylist(1);

            //Assert
            Assert.IsInstanceOfType(newView, typeof(ViewResult));
        }

        [TestMethod]
        public void CreateStylish_ReturnsCorrectView_True()
        {
            //Arrange
            StylistsController controller = new StylistsController();

            //Act
            ActionResult newView = controller.Create("Sophie");

            //Assert
            Assert.IsInstanceOfType(newView, typeof(RedirectToActionResult));
        }

        //[TestMethod]
        //public void DeleteAllStylists_ReturnsCorrectView_True()
        //{
        //    //Arrange
        //    StylistsController controller = new StylistsController();

        //    //Act
        //    ActionResult newView = controller.DeleteAll();

        //    //Assert
        //    Assert.IsInstanceOfType(newView, typeof(RedirectToActionResult));
        //}

    }
}