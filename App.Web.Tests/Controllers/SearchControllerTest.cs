using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using App.Web.Controllers;
using App.Core.Services;
using Moq;
using MvcContrib.TestHelper;
using System.Web.Mvc;
using System.Collections.Generic;
using App.Core.Models;

namespace App.Web.Tests
{
    [TestClass]
    public class SearchControllerTest
    {
        private SearchController controller;

        private Mock<IWeatherService> weatherServiceMock;

        [TestInitialize]
        public void TestStartUp()
        {
            this.weatherServiceMock = new Mock<IWeatherService>();

            this.controller = new SearchController(this.weatherServiceMock.Object);
        }

        [TestMethod]
        public void Index_Returns_Default_View_With_Locations()
        {
            // Arrange
            IList<Location> locations = new List<Location>
            {
                new Location { Country = "country", City = "city", State = "state", Point = "point" }
            };
            this.weatherServiceMock.Setup(x => x.SearchLocations("q")).Returns(locations);

            // Act
            var result = this.controller.Index("q");

            // Assert
            var viewData = result.AssertResultIs<ViewResult>().ForView("").ViewData;
            Assert.AreEqual(viewData["Results"], locations);
        }
    }
}
