using System.Diagnostics;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SAVE.Website.Controllers;

using SAVE.Dal.Entities;
using SAVE.Dal.Repositories;

namespace SAVE_Website.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            var controller = new HomeController(new Repository<Action>());

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            Debug.Assert(result != null, "result != null");
            Assert.AreEqual("Modify this template to jump-start your ASP.NET MVC application.", result.ViewBag.Message);
        }

        [TestMethod]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController(new Repository<Action>());

            // Act
            //ViewResult result = controller.About() as ViewResult;

            // Assert
            //Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController(new Repository<Action>());

            // Act
            //ViewResult result = controller.Contact() as ViewResult;

            // Assert
            //Assert.IsNotNull(result);
        }
    }
}
